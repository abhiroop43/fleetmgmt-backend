using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetMgmt.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Accident")]
    public class AccidentController : Controller
    {
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