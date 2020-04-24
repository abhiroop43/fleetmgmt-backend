using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IAccidentRepository
    {
        Task<List<Accident>> GetAllAccidents();

        Task<int> AddAccident(Accident newAccident);

        Task<int> UpdateAccident(Guid accidentId, Accident updatedAccidentInfo);

        Task<int> RemoveAccident(Guid accidentId);

        Task<List<Accident>> GetAllAccidentsForVehicle(string vehicleId);

        Task<Accident> GetAccidentById(Guid accidentId);

        Task<int> PayAccidentFine(Guid accidentId);
    }
}