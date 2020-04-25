using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;
using FleetMgmt.Dto;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<ServiceResponse> AddVehicle(Vehicle newVehicle);

        Task<ServiceResponse> UpdateVehicle(string vehicleId, Vehicle updatedVehicleInfo);

        Task<ServiceResponse> RemoveVehicle(string vehicleId);

        Task<List<Vehicle>> GetAllVehicles();

        Task<Vehicle> GetVehicleById(string vehicleId);
        Task<List<Accident>> GetAllAccidentsForVehicle(string vehicleId);
    }
}