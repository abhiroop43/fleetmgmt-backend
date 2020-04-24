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
    [Route("api/Vehicle")]
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IMapper mapper, IVehicleRepository vehicleRepository)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
        }
        
        [HttpGet]
        [Route("getallvehicles")]
        public async Task<ActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleRepository.GetAllVehicles();

            var retVal = _mapper.Map<List<VehicleDto>>(vehicles);

            return Ok(retVal);
        }
        
        [HttpGet]
        [Route("getvehiclebyid/{id}")]
        public async Task<ActionResult> GetVehicleById(Guid id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);

            return Ok(_mapper.Map<VehicleDto>(vehicle));
        }

        [HttpPost]
        [Route("addnewvehicle")]
        public async Task<ActionResult> AddNewVehicle([FromBody] VehicleDto newvehicle)
        {
            var vehicle = _mapper.Map<Vehicle>(newvehicle);
            return Ok(await _vehicleRepository.AddVehicle(vehicle));
        }

        [HttpPut]
        [Route("updatevehicle/{id}")]
        public async Task<ActionResult> UpdateVehicle(Guid id, [FromBody] VehicleDto updatedVehicleInfo)
        {
            var vehicle = _mapper.Map<Vehicle>(updatedVehicleInfo);

            return Ok(await _vehicleRepository.UpdateVehicle(id, vehicle));
        }

        [HttpDelete]
        [Route("deletevehicle/{id}")]
        public async Task<ActionResult> DeleteVehicle(Guid id)
        {
            return Ok(await _vehicleRepository.RemoveVehicle(id));
        }

        [HttpGet]
        [Route("getallaccidentsofvehicle/{id}")]
        public async Task<ActionResult> GetAllAccidentsOfVehicle(Guid id)
        {
            return Ok(await _vehicleRepository.GetAllAccidentsForVehicle(id));
        }
    }
}