using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Chess.Core.Models;
using Chess.Core.Repositories;
using Chess.Web.Models.GameModels;
using Microsoft.AspNetCore.Authorization;

namespace Chess.Web.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public StatisticController(IUnitOfWork unitOfWork) => _uow = unitOfWork;

        [HttpGet]
        public ActionResult<Player[]> TopPlayersBlitz()
        {
            return _uow.PlayerRepository.All().OrderByDescending(x=>x.PlayerDetails.BlitzRating).Take(10).ToArray();
        }

        [HttpGet]
        public ActionResult<Player[]> TopPlayersBullet()
        {
            return _uow.PlayerRepository.All().OrderByDescending(x => x.PlayerDetails.BulletRating).Take(10).ToArray();
        }

        [HttpGet]
        public ActionResult<Player[]> TopPlayersRapid()
        {
            return _uow.PlayerRepository.All().OrderByDescending(x => x.PlayerDetails.RapidRating).Take(10).ToArray();
        }

        [HttpGet]
        public ActionResult<Player[]> TopPlayersKnightsMode()
        {
            return _uow.PlayerRepository.All().OrderByDescending(x => x.PlayerDetails.OnlyKnightsRating).Take(10).ToArray();
        }

        [HttpGet]
        public ActionResult<Player[]> TopPlayersPawnsMode()
        {
            return _uow.PlayerRepository.All().OrderByDescending(x => x.PlayerDetails.OnlyPawnsRating).Take(10).ToArray();
        }

        [HttpGet]
        public ActionResult<Player[]> TopPlayersQueensMode()
        {
            return _uow.PlayerRepository.All().OrderByDescending(x => x.PlayerDetails.OnlyQueensRating).Take(10).ToArray();
        }

        [HttpGet]
        public ActionResult<HistoryModel[]> GamesHistory()
        {
            var id = HttpContext.User.Claims.Where(x => x.Type == "Id").Select(c => c.Value).First();
            var history = _uow.GameHistoryRepository
                .All(z=>z.BlackPlayerId.ToString() == id || z.WhitePlayerId.ToString() == id)
                .OrderByDescending(x => x.GameDate).ToArray();
            var z = _uow.GameHistoryRepository.All().ToList();

            var historyModel = new List<HistoryModel>();
            foreach (var x in history)
            {
                var opponent = x.WhitePlayerId.ToString() == id ? x.BlackPlayer : x.WhitePlayer;
                string result;
                if (x.Winner == null)
                {
                    result = "Draw";
                }
                else if (x.WinnerId.ToString() == id)
                {
                    result = "Win";
                }
                else
                {
                    result = "Lose";
                }

                historyModel.Add(new HistoryModel()
                {
                    GameDate = x.GameDate.ToString("g"),
                    ResultType = x.EndGameType.Type,
                    Result = result,
                    OpponentNickname = opponent.Nickname,
                    GameType = x.GameType.Type

                });
            }

            return historyModel.ToArray();
        }
    }
}
