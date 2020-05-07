using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;
using FleetMgmt.Dto;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<ServiceResponse> AddVehicle(VehicleDto newVehicle);

        Task<ServiceResponse> UpdateVehicle(string vehicleId, VehicleDto updatedVehicleInfo);

        Task<ServiceResponse> RemoveVehicle(string vehicleId);

        Task<ServiceResponse> GetAllVehicles(SearchInputDto searchInput);

        Task<Vehicle> GetVehicleById(string vehicleId);

        Task<ServiceResponse> GetVehicleByIdReadOnly(string vehicleId);

        Task<List<Accident>> GetAllAccidentsForVehicle(string vehicleId);
    }
}