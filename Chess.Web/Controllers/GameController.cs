using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Core.Repositories;
using Chess.Web.Models.GameModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using EndGameType = Chess.Web.Models.GameModels.EndGameType;
using GameType = Chess.Core.GameModels.GameType;

namespace Chess.Web.Controllers
{
    [Authorize]
    [Route("Game")]
    public class GameController : Controller
    {
        private static IUnitOfWork _uowStatic;
        private readonly IUnitOfWork _uow;

        public GameController(IUnitOfWork unitOfWork)
        {
            _uowStatic = unitOfWork;
            _uow = unitOfWork;
        }

        [HttpGet("GameLobby")]
        public IActionResult GameLobby()
        {
            var id = HttpContext.User.Claims.Where(x => x.Type == "PlayerDetailsId").Select(c => c.Value).First();
            ViewBag.Rating = _uow.PlayerDetailsRepository.Get(new Guid(id));
            ViewBag.IsBlockedChat =
                HttpContext.User.Claims.Where(x => x.Type == "Status").Select(c => c.Value).First() == "BlockedChat";
            return View();
            

        }

        [HttpPost("EndGame")]
        public  IActionResult EndGame(EndGameModel model)
        {
            if (model.EndGameType != EndGameType.DrawMutualConsent)
            {
                var winner = _uow.PlayerRepository.FindByNickname(model.User1Nickname); 
                var looser = _uow.PlayerRepository.FindByNickname(model.User2Nickname);

                var blackPlayerWin = model.User2Nickname == model.WinnerNickname;

            if (blackPlayerWin)
            {
                 winner = _uow.PlayerRepository.FindByNickname(model.User2Nickname);
                 looser = _uow.PlayerRepository.FindByNickname(model.User1Nickname);
            }

            switch (model.GameType)
            {
                case GameType.Bullet:
                    winner.PlayerDetails.BulletRating += 10;
                    looser.PlayerDetails.BulletRating -= 10;
                    break;
                case GameType.Blitz:
                    winner.PlayerDetails.BlitzRating += 10;
                    looser.PlayerDetails.BlitzRating -= 10;
                    break;
                case GameType.Rapid:
                    winner.PlayerDetails.RapidRating += 10;
                    looser.PlayerDetails.RapidRating -= 10;
                    break;
                case GameType.OnlyPawnsMode:
                    winner.PlayerDetails.OnlyPawnsRating += 10;
                    looser.PlayerDetails.OnlyPawnsRating -= 10;
                    break;
                case GameType.OnlyKnightsMode:
                    winner.PlayerDetails.OnlyKnightsRating += 10;
                    looser.PlayerDetails.OnlyKnightsRating -= 10;
                    break;
                case GameType.OnlyQueensMode:
                    winner.PlayerDetails.OnlyQueensRating += 10;
                    looser.PlayerDetails.OnlyQueensRating -= 10;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }

            var gameHistory = new GameHistory()
            {
                Id = Guid.NewGuid(),
                GameDate = DateTime.Now,
                WhitePlayer = _uow.PlayerRepository.FindByNickname(model.User1Nickname),
                BlackPlayer = _uow.PlayerRepository.FindByNickname(model.User2Nickname),
                GameTypeId = (int) model.GameType,
                Winner = null,
                EndGameTypeId = (int) model.EndGameType
            };
            if (model.WinnerNickname != null)
            {
                gameHistory.Winner = _uow.PlayerRepository.FindByNickname(model.WinnerNickname);
            }
            _uow.GameHistoryRepository.Insert(gameHistory);
            _uow.Commit();
            return Ok();
        }

    }
}
