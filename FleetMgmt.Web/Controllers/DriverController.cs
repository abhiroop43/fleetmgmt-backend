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
    [Route("api/Driver")]
    [Authorize]
    public class DriverController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;

        public DriverController(IMapper mapper, IDriverRepository driverRepository)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
        }

        [HttpGet]
        [Route("getalldrivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _driverRepository.GetAllDrivers();

            var retVal = _mapper.Map<List<DriverDto>>(drivers);

            return Ok(retVal);
        }

        [HttpPost]
        [Route("addnewdriver")]
        public async Task<IActionResult> AddNewDriver([FromBody] DriverDto newDriver)
        {
            var driver = _mapper.Map<Driver>(newDriver);
            var retVal = await _driverRepository.AddDriver(driver);
            return Ok(retVal);
        }

//        [Route("getalltripsbydriver/{id}")]
//        public async Task<IActionResult> GetAllTripsByDriver(Guid id)
//        {
//            return Ok(_driverRepository.GetAllTripsByDriver(id));
//        }
        [HttpGet]
        [Route("getallaccidentsbydriver/{id}")]
        public async Task<IActionResult> GetAllAccidentsByDriver(string id)
        {
            return Ok(await _driverRepository.GetAllAccidentsByDriver(id));
        }

        [HttpPut]
        [Route("updatedriver/{id}")]
        public async Task<IActionResult> UpdateDriver(string id, [FromBody] DriverDto driver)
        {
            var updatedDriver = _mapper.Map<Driver>(driver);

            return Ok(await _driverRepository.UpdateDriver(id, updatedDriver));
        }

        [HttpDelete]
        [Route("deletedriver/{id}")]
        public async Task<IActionResult> DeleteDriver(string id)
        {
            return Ok(await _driverRepository.RemoveDriver(id));
        }
    }
}