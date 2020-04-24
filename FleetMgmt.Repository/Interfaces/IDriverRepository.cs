using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IDriverRepository
    {
        Task<int> AddDriver(Driver newDriver);

        Task<int> UpdateDriver(string driverId, Driver updatedDriverInfo);

        Task<int> RemoveDriver(string driverId);

        Task<List<Driver>> GetAllDrivers();

        Task<Driver> GetDriverById(string driverId);

        Task<decimal?> GetTotalFinesOfDriver(string driverId);

        Task<List<Vehicle>> GetAllVehiclesDrivenByDriver(string driverId);

        Task<List<Accident>> GetAllAccidentsByDriver(string driverId);

        [Obsolete]
        Task<List<Trip>> GetAllTripsByDriver(string driverId);
    }
}