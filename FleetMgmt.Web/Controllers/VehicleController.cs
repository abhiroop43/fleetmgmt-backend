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
        
        [HttpPost]
        [Route("getallvehicles")]
        public async Task<ActionResult> GetAllVehicles(SearchInputDto searchInput)
        {
            var vehicles = await _vehicleRepository.GetAllVehicles(searchInput);

            return Ok(vehicles);
        }
        
        [HttpGet]
        [Route("getvehiclebyid/{id}")]
        public async Task<ActionResult> GetVehicleById(string id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);

            return Ok(vehicle);
        }

        [HttpPost]
        [Route("addnewvehicle")]
        public async Task<ActionResult> AddNewVehicle([FromBody] VehicleDto newVehicle)
        {
            return Ok(await _vehicleRepository.AddVehicle(newVehicle));
        }

        [HttpPut]
        [Route("updatevehicle/{id}")]
        public async Task<ActionResult> UpdateVehicle(string id, [FromBody] VehicleDto updatedVehicleInfo)
        {
            return Ok(await _vehicleRepository.UpdateVehicle(id, updatedVehicleInfo));
        }

        [HttpDelete]
        [Route("deletevehicle/{id}")]
        public async Task<ActionResult> DeleteVehicle(string id)
        {
            return Ok(await _vehicleRepository.RemoveVehicle(id));
        }

        [HttpGet]
        [Route("getallaccidentsofvehicle/{id}")]
        public async Task<ActionResult> GetAllAccidentsOfVehicle(string id)
        {
            return Ok(await _vehicleRepository.GetAllAccidentsForVehicle(id));
        }
    }
}