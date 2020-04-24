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
    public class DriverRepository : IDriverRepository
    {
        private readonly FmDbContext _dbContext;

        public DriverRepository(FmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddDriver(Driver newDriver)
        {
            newDriver.Id = Guid.NewGuid().ToString();
            await _dbContext.Drivers.AddAsync(newDriver);

            return await SaveChanges();
        }

        public async Task<List<Accident>> GetAllAccidentsByDriver(string driverId)
        {
            return await _dbContext.Accidents.Where(a => a.Trip.DiverId == driverId).ToListAsync();
        }

        public async Task<List<Driver>> GetAllDrivers()
        {
            return await _dbContext.Drivers.Where(d => d.IsActive).ToListAsync();
        }

        [Obsolete]
        public async Task<List<Trip>> GetAllTripsByDriver(string driverId)
        {
            return await _dbContext.Trips.Where(t => t.DiverId == driverId).ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehiclesDrivenByDriver(string driverId)
        {
            return await _dbContext.Trips.Where(t => t.DiverId == driverId).Select(t => t.Vehicle).ToListAsync();
        }

        public async Task<Driver> GetDriverById(string driverId)
        {
            return await _dbContext.Drivers.FindAsync(driverId);
        }

        public async Task<decimal?> GetTotalFinesOfDriver(string driverId)
        {
            List<Accident> accidents = await GetAllAccidentsByDriver(driverId);

            return accidents.Sum(a => a.Fine);
        }

        public async Task<int> RemoveDriver(string driverId)
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

            return await SaveChanges();
        }

        public async Task<int> UpdateDriver(string driverId, Driver updatedDriverInfo)
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
            return await SaveChanges();
        }

        private async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
