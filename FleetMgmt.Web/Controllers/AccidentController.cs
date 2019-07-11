using System;
using System.Threading.Tasks;
using AutoMapper;
using FleetMgmt.Dto;
using FleetMgmt.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetMgmt.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Accident")]
    [Authorize]
    public class AccidentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccidentRepository _accidentRepository;

        public AccidentController(IMapper mapper, IAccidentRepository accidentRepository)
        {
            _mapper = mapper;
            _accidentRepository = accidentRepository;
        }

        [HttpGet]
        [Route("getallaccidents")]
        public async Task<IActionResult> GetAllAccidents()
        {
            return Ok(await _accidentRepository.GetAllAccidents());
        }

        [HttpGet]
        [Route("getaccidentbyid/{id}")]
        public async Task<IActionResult> GetAccidentById(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("getaccidentsfordriver/{id}")]
        public async Task<IActionResult> GetAccidentsForDriver(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("getaccidentsforvehicle/{id}")]
        public async Task<IActionResult> GetAccidentsForVehicle(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("addnewaccident")]
        public async Task<IActionResult> AddNewAccident([FromBody]AccidentDto newAccident)
        {
            return Ok();
        }

        [HttpPut]
        [Route("updateaccident/{id}")]
        public async Task<IActionResult> UpdateAccident(Guid id, [FromBody]AccidentDto accident)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("deleteaccident/{id}")]
        public async Task<IActionResult> DeleteAccident(Guid id)
        {
            return Ok();
        }
    }
}