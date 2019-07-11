using System;
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
    [Route("api/Accident")]
    [Authorize]
    public class AccidentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccidentRepository _accidentRepository;
        private readonly IDriverRepository _driverRepository;

        public AccidentController(IMapper mapper, IAccidentRepository accidentRepository, IDriverRepository driverRepository)
        {
            _mapper = mapper;
            _accidentRepository = accidentRepository;
            _driverRepository = driverRepository;
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
            return Ok(await _accidentRepository.GetAccidentById(id));
        }

        [HttpGet]
        [Route("getaccidentsfordriver/{id}")]
        public async Task<IActionResult> GetAccidentsForDriver(Guid id)
        {
            return Ok(await _driverRepository.GetAllAccidentsByDriver(id));
        }

        [HttpGet]
        [Route("getaccidentsforvehicle/{id}")]
        public async Task<IActionResult> GetAccidentsForVehicle(Guid id)
        {
            return Ok(await _accidentRepository.GetAllAccidentsForVehicle(id));
        }

        [HttpPost]
        [Route("addnewaccident")]
        public async Task<IActionResult> AddNewAccident([FromBody]AccidentDto newAccident)
        {
            var accident = _mapper.Map<Accident>(newAccident);
            accident.Id = Guid.NewGuid();
            return Ok(await _accidentRepository.AddAccident(accident));
        }

        [HttpPut]
        [Route("updateaccident/{id}")]
        public async Task<IActionResult> UpdateAccident(Guid id, [FromBody]AccidentDto accident)
        {
            var updatedAccident = _mapper.Map<Accident>(accident);
            return Ok(await _accidentRepository.UpdateAccident(id, updatedAccident));
        }

        [HttpDelete]
        [Route("deleteaccident/{id}")]
        public async Task<IActionResult> DeleteAccident(Guid id)
        {
            return Ok(await _accidentRepository.RemoveAccident(id));
        }
    }
}