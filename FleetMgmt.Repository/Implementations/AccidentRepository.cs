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
    public class AccidentRepository : IAccidentRepository
    {
        private readonly FmDbContext _dbContext;

        public AccidentRepository(FmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Accident>> GetAllAccidents()
        {
            return await _dbContext.Accidents.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<int> AddAccident(Accident newAccident)
        {
            var tripOfAccident = newAccident.Trip;
            newAccident.Id = Guid.NewGuid().ToString();
            if (newAccident.AccidentTime > tripOfAccident.TripDate)
            {
                await _dbContext.Accidents.AddAsync(newAccident);
                return await SaveChanges();
            }

            throw new Exception("The associated trip is not valid");
        }

        public async Task<Accident> GetAccidentById(Guid accidentId)
        {
            return await _dbContext.Accidents.FindAsync(accidentId);
        }

        public async Task<List<Accident>> GetAllAccidentsForVehicle(Guid vehicleId)
        {
            return await _dbContext.Accidents.Where(a => a.Trip.VehicleId == vehicleId).ToListAsync();
        }

        public async Task<int> PayAccidentFine(Guid accidentId)
        {
            var accident = await GetAccidentById(accidentId);

            accident.Fine = 0;

            _dbContext.Entry(accident).State = EntityState.Modified;
            return await SaveChanges();
        }

        public async Task<int> RemoveAccident(Guid accidentId)
        {
            var accident = await GetAccidentById(accidentId);

            accident.IsActive = false;
            _dbContext.Entry(accident).State = EntityState.Modified;
            return await SaveChanges();
        }

        public async Task<int> UpdateAccident(Guid accidentId, Accident updatedAccidentInfo)
        {
            var accident = await GetAccidentById(accidentId);

            accident.Fine = updatedAccidentInfo.Fine;
            accident.CoveredByInsurance = updatedAccidentInfo.CoveredByInsurance;
            accident.AccidentTime = updatedAccidentInfo.AccidentTime;
            accident.IsOwnFault = updatedAccidentInfo.IsOwnFault;
            accident.TripId = updatedAccidentInfo.TripId;

            _dbContext.Entry(accident).State = EntityState.Modified;
            return await SaveChanges();
        }

        private async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
