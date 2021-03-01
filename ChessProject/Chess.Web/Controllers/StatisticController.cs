using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Chess.Web.Controllers
{
    [Authorize]
    [Route("Statistic")]
    public class StatisticController : Controller
    {

        [HttpGet("Players")]
        public IActionResult TopPlayers()
        {
            return View();
        }
    }
}
