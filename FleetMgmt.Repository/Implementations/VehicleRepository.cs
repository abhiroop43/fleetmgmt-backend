using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FleetMgmt.Data;
using FleetMgmt.Data.Entities;
using FleetMgmt.Domain;
using FleetMgmt.Dto;
using FleetMgmt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace FleetMgmt.Repository.Implementations
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly FmDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSession _userSession;
        private readonly IMapper _mapper;

        public VehicleRepository(FmDbContext dbContext, IUnitOfWork unitOfWork, IUserSession userSession, IMapper mapper)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _userSession = userSession;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddVehicle(VehicleDto newVehicle)
        {
            var vehicle = _mapper.Map<Vehicle>(newVehicle);

            ServiceResponse response = null;
            vehicle.Id = Guid.NewGuid().ToString();
            vehicle.CreatedDate = DateTime.Now;
            vehicle.CreatedBy = _userSession.GetUser()?.Name;

            await _dbContext.Vehicles.AddAsync(vehicle);
            _unitOfWork.SetIsActive(true);
            
            var savedRecords =  await _unitOfWork.CommitAsync();

            if (savedRecords > 0)
            {
                response = new ServiceResponse
                {
                    Success = true,
                    Message = "Vehicle Added successfully"
                };
            }
            else
            {
                response  = new ServiceResponse
                {
                    Success = false,
                    Message = "Vehicle not saved"
                };
            }

            return response;
        }

        public async Task<List<Accident>> GetAllAccidentsForVehicle(string vehicleId)
        {
            return await _dbContext.Accidents.Where(a => a.Trip.VehicleId == vehicleId).ToListAsync();
        }

        public async Task<ServiceResponse> GetAllVehicles(SearchInputDto searchInput)
        {
            Expression<Func<Vehicle, bool>> expression = x => x.IsActive;
            if (searchInput.Filters != null && searchInput.Filters.Any())
            {
                var dynamicQuery = Helper.QueryMapper(searchInput.Filters, "VehicleSearch.json");

                if (!string.IsNullOrEmpty(dynamicQuery))
                {
                    Expression<Func<Vehicle, bool>> query = DynamicExpressionParser.ParseLambda<Vehicle, bool>(new ParsingConfig(), false, dynamicQuery);
                    expression = DynamicExpressionParser.ParseLambda<Vehicle, bool>(new ParsingConfig(), false, "@0(it) and @1(it)", expression, query);
                }
            }

            IQueryable<Vehicle> vehicles = _dbContext.Vehicles.Where(expression);

            var vehiclesForTotalCount = vehicles;
            int totalCount = await vehiclesForTotalCount.CountAsync();

            var searchedVehicles = await  vehicles
                .Skip(searchInput.PageSize * (searchInput.PageNumber - 1)).Take(searchInput.PageSize).ToListAsync();

            var mappedVehicles = _mapper.Map<List<VehicleDto>>(searchedVehicles);

            var pageDetails =
                new PagedData<Vehicle>().GetPaginationDetails<Vehicle>(searchInput.PageNumber, searchInput.PageSize,
                    totalCount);

            var tempPageData = new PagedData<VehicleDto>()
            {
                Data = mappedVehicles,
                HasNextPage = pageDetails.HasNextPage,
                HasPreviousPage = pageDetails.HasPreviousPage,
                IsFirstPage = pageDetails.IsFirstPage,
                IsLastPage = pageDetails.IsLastPage,
                ItemEnd = pageDetails.ItemEnd,
                ItemStart = pageDetails.ItemStart,
                PageCount = pageDetails.PageCount,
                PageIndex = pageDetails.PageIndex,
                PageSize = pageDetails.PageSize,
                TotalItemCount = pageDetails.TotalItemCount
            };
            var response = new ServiceResponse {Data = tempPageData, Success = true};
            response.Message = response.Success ? "Vehicles list successfully loaded..." : "No vehicle data found..";

            return response;
        }

        public async Task<Vehicle> GetVehicleById(string vehicleId)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new Exception("No vehicle with the given Id was found");
            }

            return vehicle;
        }

        public async Task<ServiceResponse> GetVehicleByIdReadOnly(string vehicleId)
        {
            var response = new ServiceResponse();
            var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new Exception("No vehicle with the given Id was found");
            }

            _dbContext.Entry(vehicle).State = EntityState.Detached;

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            response.Message = "Vehicle details loaded successfully";
            response.Data = vehicleDto;
            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse> RemoveVehicle(string vehicleId)
        {
            ServiceResponse response = null;
            var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new Exception("No vehicle with the given Id was found");
            }

            vehicle.IsActive = false;
            vehicle.UpdatedDate = DateTime.Now;
            vehicle.UpdatedBy = _userSession.GetUser()?.Name;

            _dbContext.Entry(vehicle).State = EntityState.Modified;
            _unitOfWork.SetIsActive(true);
            var affectedRows =  await _unitOfWork.CommitAsync();

            if (affectedRows > 0)
            {
                response = new ServiceResponse
                {
                    Success = true,
                    Message = "Vehicle Deleted successfully"
                };
            }
            else
            {
                response  = new ServiceResponse
                {
                    Success = false,
                    Message = "Failed to delete vehicle"
                };
            }

            return response;
            // return await SaveChanges();
        }

        public async Task<ServiceResponse> UpdateVehicle(string vehicleId, VehicleDto updatedVehicleInfo)
        {
            ServiceResponse response = null;

            // var existingVehicle = _mapper.Map<Vehicle>(updatedVehicleInfo);

            var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new Exception("No vehicle with the given Id was found");
            }

            vehicle.ChassisNumber = updatedVehicleInfo.ChassisNumber;
            vehicle.Color = updatedVehicleInfo.Color;
            vehicle.EngineNumber = updatedVehicleInfo.EngineNumber;
            vehicle.PlateNumber = updatedVehicleInfo.PlateNumber;
            vehicle.Make = updatedVehicleInfo.Make;
            vehicle.Model = updatedVehicleInfo.Model;
            vehicle.UpdatedDate = DateTime.Now;
            vehicle.UpdatedBy = _userSession.GetUser()?.Name;

            _dbContext.Entry(vehicle).State = EntityState.Modified;
            _unitOfWork.SetIsActive(true);
            var affectedRows = await _unitOfWork.CommitAsync();

            if (affectedRows > 0)
            {
                response = new ServiceResponse
                {
                    Success = true,
                    Message = "Vehicle Updated successfully"
                };
            }
            else
            {
                response  = new ServiceResponse
                {
                    Success = false,
                    Message = "Failed to update vehicle"
                };
            }

            return response;

            // return await SaveChanges();
        }

        private async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
