using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FleetMgmt.Data.Entities;

namespace FleetMgmt.Repository.Interfaces
{
    public interface ITripRepository
    {
        Task<int> AddTrip(Trip newTrip);

        Task<int> UpdateTrip(string tripId, Trip updatedTripInfo);

        Task<int> RemoveTrip(string tripId);

        Task<List<Trip>> GetAllTripsForDriver(string driverId);

        Task<List<Trip>> GetAllTripsForVehicle(string vehicleGuid);

        Task<Trip> GetTripById(string tripId);
    }
}