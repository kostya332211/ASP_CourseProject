
namespace Chess.Web.Models.GameModels
{
    public enum EndGameType
    {
        Checkmate = 1,
        Stalemate,
        LeaveOpponent,
        TimeIsOver,
        DrawMutualConsent
    }
}
