using Microsoft.AspNetCore.Mvc;
using Chess.Engine;
using Chess.Web.Models.EngineModels;

namespace Chess.Web.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        private const int MaxThinkingTime = 4000;

        public ActionResult<ResponseEngineModel> BestMove(RequestEngineModel model)
        {
            var responseModel = new ResponseEngineModel();
            model.FEN ??= Constants.Position.Default;
            var board = new BoardData(model.FEN);
            var engine = new Engine.Engine(board);
            responseModel.BestMove = engine.PerformBestMove(model.Depth, MaxThinkingTime);
            return Ok(responseModel);
        }
    }

}
