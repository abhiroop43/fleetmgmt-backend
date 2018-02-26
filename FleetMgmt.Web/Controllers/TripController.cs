using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetMgmt.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Trip")]
    public class TripController : Controller
    {
        //// Busines Logic: 
        /// 1. a driver cannot be reserved for multiple trips on the same time period
        /// 2. a driver cannot drive for more than 10 hrs a day
        /// 3. a driver must have at least one day off in a week
    }
}