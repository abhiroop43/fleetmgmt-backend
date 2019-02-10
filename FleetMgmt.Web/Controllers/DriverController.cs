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
            driver.Id = Guid.NewGuid();

            var retVal = await _driverRepository.AddDriver(driver);

            return Ok(retVal);
        }

        [Route("getalltripsbydriver/{id}")]
        public async Task<IActionResult> GetAllTripsByDriver(Guid id)
        {
            return Ok(_driverRepository.GetAllTripsByDriver(id));
        }

        [Route("getallaccidentsbydriver/{id}")]
        public async Task<IActionResult> GetAllAccidentsByDriver(Guid id)
        {
            return Ok(_driverRepository.GetAllAccidentsByDriver(id));
        }
    }
}