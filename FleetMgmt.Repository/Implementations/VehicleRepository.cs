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
    public class VehicleRepository : IVehicleRepository
    {
        private readonly FmDbContext _dbContext;
        public VehicleRepository(FmDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddVehicle(Vehicle newVehicle)
        {
            newVehicle.Id = Guid.NewGuid().ToString();
            await _dbContext.Vehicles.AddAsync(newVehicle);
            return await SaveChanges();
        }

        public async Task<List<Accident>> GetAllAccidentsForVehicle(Guid vehicleId)
        {
            return await _dbContext.Accidents.Where(a => a.Trip.VehicleId == vehicleId).ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _dbContext.Vehicles.Where(v => v.IsActive).ToListAsync();
        }

        public async Task<Vehicle> GetVehicleById(Guid vehicleId)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new Exception("No vehicle with the given Id was found");
            }

            return vehicle;
        }

        public async Task<int> RemoveVehicle(Guid vehicleId)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new Exception("No vehicle with the given Id was found");
            }

            vehicle.IsActive = false;

            _dbContext.Entry(vehicle).State = EntityState.Modified;
            return await SaveChanges();
        }

        public async Task<int> UpdateVehicle(Guid vehicleId, Vehicle updatedVehicleInfo)
        {
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

            _dbContext.Entry(vehicle).State = EntityState.Modified;
            return await SaveChanges();
        }

        private async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
