
namespace Chess.Web.Models.GameModels
{
    public class EndGameModel
    {
        public string User1Nickname { get; set; }

        public string User2Nickname { get; set; }
        
        public string WinnerNickname { get; set; }
        
        public Chess.Core.GameModels.GameType GameType { get; set; }
        
        public EndGameType EndGameType { get; set; }
    }
}
