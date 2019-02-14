using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;

namespace FleetMgmt.Repository.Interfaces
{
    public interface ITripRepository
    {
        Task<int> AddTrip(Trip newTrip);

        Task<int> UpdateTrip(Guid tripId, Trip updatedTripInfo);

        Task<int> RemoveTrip(Guid tripId);

        Task<List<Trip>> GetAllTripsForDriver(Guid driverId);

        Task<List<Trip>> GetAllTripsForVehicle(Guid vehicleGuid);

        Task<Trip> GetTripById(Guid tripId);
    }
}