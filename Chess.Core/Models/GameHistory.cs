using System;

namespace Chess.Core.Models
{
    public class GameHistory : Entity<Guid>
    {
        public int GameTypeId { get; set; }

        public Guid WhitePlayerId { get; set; }

        public Guid BlackPlayerId { get; set; }

        public Guid? WinnerId { get; set; }

        public DateTime GameDate { get; set; }

        public int EndGameTypeId { get; set; }

        #region Navigation properties

        public GameType GameType { get; set; }

        public Player WhitePlayer { get; set; }

        public Player BlackPlayer { get; set; }

        public Player Winner { get; set; }

        public EndGameType EndGameType { get; set; }

        #endregion
    }
}
