using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IDriverRepository
    {
        Task<int> AddDriver(Driver newDriver);

        Task<int> UpdateDriver(Guid driverId, Driver updatedDriverInfo);

        Task<int> RemoveDriver(Guid driverId);

        Task<List<Driver>> GetAllDrivers();

        Task<Driver> GetDriverById(Guid driverId);

        Task<decimal?> GetTotalFinesOfDriver(Guid driverId);

        Task<List<Vehicle>> GetAllVehiclesDrivenByDriver(Guid driverId);

        Task<List<Accident>> GetAllAccidentsByDriver(Guid driverId);

        Task<List<Trip>> GetAllTripsByDriver(Guid driverId);
    }
}