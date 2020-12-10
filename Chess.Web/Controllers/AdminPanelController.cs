using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Chess.Web.Controllers
{
    [Route("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IUnitOfWork _uow;

        public AdminPanelController(IUnitOfWork unitOfWork) => _uow = unitOfWork;

        [HttpGet("Panel")]
        public IActionResult Panel()
        {
            var players = _uow.PlayerRepository.All().ToList();
            return View(players);
        }
    }
}
