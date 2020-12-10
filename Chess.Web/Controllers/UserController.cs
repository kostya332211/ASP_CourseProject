using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Core.Repositories;
using Chess.Web.Helpers;
using Chess.Web.Models.ValidationModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Chess.Web.Controllers
{

    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UserController(IUnitOfWork unitOfWork) => _uow = unitOfWork;

        [Authorize]
        [HttpGet("Profile")]
        public IActionResult UserProfile()
        {
            var model = new ChangePasswordValidationModel();
            return View(model);
        }

        [HttpGet("LogIn")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginValidationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _uow.PlayerRepository.FindByEmail(model.Email.ToLower());

            if(user == null)
            {
                ModelState.AddModelError("Email", "User with this email not founded");
                return View(model);
            }

            if(Sha512Hash.ComputeHash(model.Password, user.PasswordSalt) != user.PasswordHash)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return View(model);
            }

            if (user.Status.StatusName == "Blocked")
            {
                ModelState.AddModelError("Email", "This email was blocked");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("PlayerDetailsId", user.PlayerDetailsId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Nickname),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim("Status", user.Status.StatusName),
            };
            
            var identity = new ClaimsIdentity(claims, "cookie");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("cookie", principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterValidationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var isInvalidModel = false;

            if (_uow.PlayerRepository.FindByEmail(model.Email.ToLower()) != null)
            {
                ModelState.AddModelError("Email", "User with this email already exist");
                isInvalidModel = true;
            }

            if (_uow.PlayerRepository.FindByNickname(model.Nickname.ToLower()) != null)
            {
                ModelState.AddModelError("Nickname", "User with this nickname already exist");
                isInvalidModel = true;
            }

            if (model.Password != model.RePassword)
            {
                ModelState.AddModelError("Password", "Password not equals");
                isInvalidModel = true;
            }

            if (isInvalidModel)
            {
                return View(model);
            }

            var salt = Sha512Hash.GenerateSalt();

            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Nickname = model.Nickname.ToLower(),
                Email = model.Email.ToLower(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordSalt = salt,
                PasswordHash = Sha512Hash.ComputeHash(model.Password, salt),
                RoleId = 1,
                StatusId = 1,
                PlayerDetails = new PlayerDetails(){ Id = Guid.NewGuid() }
            };

            _uow.PlayerRepository.Insert(player);
            _uow.Commit();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("cookie");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordValidationModel model)
        {
            if (!ModelState.IsValid)
                return View("UserProfile", model);

            if (model.Password !=  model.RePassword)
            {
                ModelState.AddModelError("Password", "Password not equals");
                return View("UserProfile", model);
            }

            var id = HttpContext.User.Claims.Where(x => x.Type == "Id").Select(c => c.Value).First();
            var player = _uow.PlayerRepository.Get(new Guid(id));
            var salt = Sha512Hash.GenerateSalt();
            player.PasswordSalt = salt;
            player.PasswordHash = Sha512Hash.ComputeHash(model.Password, salt);
            _uow.Commit();
            return RedirectToAction("Index", "Home");
        }

    }
}
