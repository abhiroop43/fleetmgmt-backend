using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FleetMgmt.Dto;
using FleetMgmt.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetMgmt.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Accident")]
    [Authorize]
    public class AccidentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        public AccidentController(IMapper mapper, IVehicleRepository vehicleRepository, IDriverRepository driverRepository)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
        }
        //// Business Logic: An accident can only happen if the driver is driving the specified vehicle during that time
        [HttpGet]
        public async Task<IActionResult> GetAllAccidents()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccidentById(Guid accidentId)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccidentsForDriver()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccidentsForVehicle()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAccident([FromBody]AccidentDto newAccident)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccident(Guid accidentId, [FromBody]AccidentDto accident)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccident(Guid accidentId)
        {
            return Ok();
        }
    }
}