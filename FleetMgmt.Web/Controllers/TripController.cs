using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetMgmt.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetMgmt.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Trip")]
    [Authorize]
    public class TripController : Controller
    {
        //// Busines Logic: 
        /// 1. a driver cannot be reserved for multiple trips on the same time period
        /// 2. a driver cannot drive for more than 10 hrs a day
        /// 3. a driver must have at least one day off in a week
        
        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTripById(Guid tripId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTrip([FromBody]TripDto newTrip)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrip(Guid tripId, [FromBody]TripDto trip)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrip(Guid tripId)
        {
            return Ok();
        }
    }
}