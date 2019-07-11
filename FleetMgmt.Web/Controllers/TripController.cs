using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FleetMgmt.Data.Entities;
using FleetMgmt.Dto;
using FleetMgmt.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetMgmt.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Trip")]
    [Authorize]
    public class TripController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITripRepository _tripRepository;
        
        public TripController(IMapper mapper, ITripRepository tripRepository)
        {
            _mapper = mapper;
            _tripRepository = tripRepository;
        }

        [HttpGet]
        [Route("getalltripsfordriver/{id}")]
        public async Task<IActionResult> GetAllTripsForDriver(Guid id)
        {
            var trips = await _tripRepository.GetAllTripsForDriver(id);

            var retVal = _mapper.Map<List<TripDto>>(trips);
            return Ok(retVal);
        }

        [HttpGet]
        [Route("getalltripsforvehicle/{id}")]
        public async Task<IActionResult> GetAllTripsForVehicle(Guid id)
        {
            var trips = await _tripRepository.GetAllTripsForVehicle(id);

            var retVal = _mapper.Map<List<TripDto>>(trips);
            return Ok(retVal);
        }

        [HttpGet]
        [Route("gettripbyid/{id}")]
        public async Task<IActionResult> GetTripById(Guid id)
        {
            var trips = await _tripRepository.GetTripById(id);

            var retVal = _mapper.Map<List<TripDto>>(trips);
            return Ok(retVal);
        }

        [HttpPost]
        [Route("addnewtrip")]
        public async Task<IActionResult> AddNewTrip([FromBody]TripDto newTrip)
        {
            var trip = _mapper.Map<Trip>(newTrip);
            trip.Id = Guid.NewGuid();

            var retVal = await _tripRepository.AddTrip(trip);

            return Ok(retVal);
        }

        [HttpPut]
        [Route("updatetrip/{id}")]
        public async Task<IActionResult> UpdateTrip(Guid id, [FromBody]TripDto trip)
        {
            var updatedTrip = _mapper.Map<Trip>(trip);

            var retVal = await _tripRepository.UpdateTrip(id, updatedTrip);
            return Ok(retVal);
        }

        [HttpDelete]
        [Route("deletetrip/{id}")]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            return Ok(await _tripRepository.RemoveTrip(id));
        }
    }
}