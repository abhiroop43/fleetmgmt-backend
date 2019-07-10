using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FleetMgmt.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetMgmt.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Accident")]
    public class AccidentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        public AccidentController()
        {
            
        }
        //// Business Logic: An accident can only happen if the driver is driving the specified vehicle during that time
        [HttpGet]
        public async Task<IActionResult> GetAllAccidents()
        {
            return Ok();
        }
    }
}