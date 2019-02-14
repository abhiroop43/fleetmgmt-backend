using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetMgmt.Data;
using FleetMgmt.Data.Entities;
using FleetMgmt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetMgmt.Repository.Implementations
{
    public class TripRepository : ITripRepository
    {
        private readonly FmDbContext _dbContext;

        public TripRepository(FmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddTrip(Trip newTrip)
        {
            _dbContext.Trips.Add(newTrip);
            return await SaveChanges();
        }

        public async Task<List<Trip>> GetAllTripsForDriver(Guid driverId)
        {
            return await _dbContext.Trips.Where(t => t.DiverId == driverId).ToListAsync();
        }

        public async Task<List<Trip>> GetAllTripsForVehicle(Guid vehicleGuid)
        {
            return await _dbContext.Trips.Where(t => t.VehicleId == vehicleGuid).ToListAsync();
        }

        public async Task<Trip> GetTripById(Guid tripId)
        {
            return await _dbContext.Trips.FindAsync(tripId);
        }

        public async Task<int> RemoveTrip(Guid tripId)
        {
            var foundTrip = await _dbContext.Trips.FindAsync(tripId);

            if (foundTrip != null)
            {
                foundTrip.IsActive = false;
                _dbContext.Entry(foundTrip).State = EntityState.Modified;
            }

            return await SaveChanges();
        }

        public async Task<int> UpdateTrip(Guid tripId, Trip updatedTripInfo)
        {
            var currentTrip = await _dbContext.Trips.FindAsync(tripId);

            if (currentTrip != null)
            {
                currentTrip.VehicleId = updatedTripInfo.VehicleId;
                currentTrip.DiverId = updatedTripInfo.DiverId;
                currentTrip.Length = updatedTripInfo.Length;
                currentTrip.TripDate = updatedTripInfo.TripDate;
                _dbContext.Entry(currentTrip).State = EntityState.Modified;
            }

            return await SaveChanges();
        }

        private async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
