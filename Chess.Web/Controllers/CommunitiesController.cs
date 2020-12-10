using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Chess.Web.Controllers
{
    [Authorize]
    [Route("Communities")]
    public class CommunitiesController : Controller
    {
        [HttpGet("Chat")]
        public IActionResult Chat()
        {
            ViewBag.IsBlockedChat =
                HttpContext.User.Claims.Where(x => x.Type == "Status").Select(c => c.Value).First() == "BlockedChat";
            return View();
        }
    }
}
