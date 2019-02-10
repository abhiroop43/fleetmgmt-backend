using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetMgmt.Data;
using FleetMgmt.Data.Entities;
using FleetMgmt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetMgmt.Repository.Implementations
{
    class DriverRepository : IDriverRepository
    {
        private readonly FmDbContext _dbContext;

        public DriverRepository(FmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddDriver(Driver newDriver)
        {
            _dbContext.Drivers.Add(newDriver);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Accident>> GetAllAccidentsByDriver(Guid driverId)
        {
            return await _dbContext.Accidents.Where(a => a.Trip.DiverId == driverId).ToListAsync();
        }

        public async Task<List<Driver>> GetAllDrivers()
        {
            return await _dbContext.Drivers.ToListAsync();
        }

        public async Task<List<Trip>> GetAllTripsByDriver(Guid driverId)
        {
            return await _dbContext.Trips.Where(t => t.DiverId == driverId).ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehiclesDrivenByDriver(Guid driverId)
        {
            return await _dbContext.Trips.Where(t => t.DiverId == driverId).Select(t => t.Vehicle).ToListAsync();
        }

        public async Task<Driver> GetDriverById(Guid driverId)
        {
            return await _dbContext.Drivers.FindAsync(driverId);
        }

        public async Task<decimal?> GetTotalFinesOfDriver(Guid driverId)
        {
            List<Accident> accidents = await GetAllAccidentsByDriver(driverId);

            return accidents.Sum(a => a.Fine);
        }

        public async Task<int> RemoveDriver(Guid driverId)
        {
            Driver driver = await GetDriverById(driverId);

            if (driver != null)
            {
                // Hard delete
                //_dbContext.Drivers.Attach(driver);
                //_dbContext.Drivers.Remove(driver);

                // Soft Delete
                driver.IsActive = false;
                _dbContext.Drivers.Update(driver);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateDriver(Guid driverId, Driver updatedDriverInfo)
        {
            Driver driver = await GetDriverById(driverId);
            if (driver != null)
            {
                driver.FirstName = updatedDriverInfo.FirstName;
                driver.LastName = updatedDriverInfo.LastName;
                driver.DateOfBirth = updatedDriverInfo.DateOfBirth;
                driver.LicenseNumber = updatedDriverInfo.LicenseNumber;
                _dbContext.Drivers.Update(driver);
                //_dbContext.Entry(driver).State = EntityState.Modified;
            }
            return await _dbContext.SaveChangesAsync();
        }
    }
}
