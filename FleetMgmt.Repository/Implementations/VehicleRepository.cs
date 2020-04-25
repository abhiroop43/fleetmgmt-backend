using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetMgmt.Data;
using FleetMgmt.Data.Entities;
using FleetMgmt.Dto;
using FleetMgmt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetMgmt.Repository.Implementations
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly FmDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSession _userSession;

        public VehicleRepository(FmDbContext dbContext, IUnitOfWork unitOfWork, IUserSession userSession)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _userSession = userSession;
        }

        public async Task<ServiceResponse> AddVehicle(Vehicle newVehicle)
        {
            ServiceResponse response = null;
            newVehicle.Id = Guid.NewGuid().ToString();
            newVehicle.CreatedDate = DateTime.Now;
            newVehicle.CreatedBy = _userSession.GetUser()?.Name;

            await _dbContext.Vehicles.AddAsync(newVehicle);
            _unitOfWork.SetIsActive(true);
            
            var savedRecords =  await _unitOfWork.SaveAsync();

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

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _dbContext.Vehicles.Where(v => v.IsActive).ToListAsync();
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
            var affectedRows =  await _unitOfWork.SaveAsync();

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

        public async Task<ServiceResponse> UpdateVehicle(string vehicleId, Vehicle updatedVehicleInfo)
        {
            ServiceResponse response = null;

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
            var affectedRows = await _unitOfWork.SaveAsync();

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
