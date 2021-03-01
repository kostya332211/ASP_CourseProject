
namespace Chess.Web.Models.EngineModels
{
    public class RequestEngineModel
    {
        public string FEN { get; set; }

        public int Depth { get; set; } = 4;
    }
}
