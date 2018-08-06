using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;
using FleetMgmt.Dto;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<int> AddVehicle(Vehicle newVehicle);

        Task<int> UpdateVehicle(Guid vehicleId, Vehicle updatedVehicleInfo);

        Task<int> RemoveVehicle(Guid vehicleId);

        Task<List<Vehicle>> GetAllVehicles();

        Task<Vehicle> GetVehicleById(Guid vehicleId);
    }
}