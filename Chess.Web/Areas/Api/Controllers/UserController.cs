using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Core.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Chess.Web.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public UserController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        [HttpGet("")]
        public ActionResult<Player> Get()
        {
            var id = HttpContext.User.Claims.Where(x => x.Type == "Id").Select(c => c.Value).First();
            return Ok(_uow.PlayerRepository.Get(new Guid(id)));
        }

        [HttpGet("GetAll")]
        public ActionResult<Player[]> GetAll()
        {
            return Ok(_uow.PlayerRepository.All().Where(x=>x.Role.RoleName != "Admin").ToArray());
        }

        [HttpPut("")]
        public ActionResult<Player> Edit()
        {
            var id = HttpContext.User.Claims.Where(x => x.Type == "Id").Select(c => c.Value).First();
            return Ok(_uow.PlayerRepository.Get(new Guid(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("BanPermanent")]
        public ActionResult<Player> BanPermanent(Guid id)
        {
            var user = _uow.PlayerRepository.Get(id);
            user.StatusId = 2;
            _uow.Commit();
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("BanChat")]
        public ActionResult<Player> BanChat(Guid id)
        {
            var user = _uow.PlayerRepository.Get(id);
            user.StatusId = 3;
            _uow.Commit();
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser")]
        public ActionResult<Player> DeleteUser(Guid id)
        {
            var user = _uow.PlayerRepository.Get(id);
            _uow.PlayerRepository.Delete(id);
            _uow.Commit();
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Amnisty")]
        public ActionResult<Player> Amnisty(Guid id)
        {
            var user = _uow.PlayerRepository.Get(id);
            user.StatusId = 1;
            _uow.Commit();
            return Ok(user);
        }
    }
}
