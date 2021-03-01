using Chess.Logic.EventArguments;
using System;
using Chess.Core.GameModels;

namespace Chess.Logic
{
    public class Game
    {
        public Game(Player player1, Player player2, GameType type)
        {
            ChessBoard = new Board();
            ChessBoard.MakeBoard(type);

            Player1 = player1;
            Player2 = player2;

            Id = Guid.NewGuid().ToString();
            this.GameType = type;

            MinutesForPlayer = type switch
            {
                GameType.Bullet => 2,
                GameType.Rapid => 10,
                _ => 5
            };

            Player1.GameId = Id;
            Player2.GameId = Id;
        }

        public Move MoveSelected(Square start, Square end)
        {
            var move = ChessBoard.MakeMove(start, end);
            if (move != null)
                ChangeTurns();

            return move;
        }

        public event EventHandler OnGameOver;

        public Player MovingPlayer => Player1?.HasToMove ?? false ? Player1 : Player2;

        public Player WaitingPlayer => Player1?.HasToMove ?? false ? Player2 : Player1;

        public Board ChessBoard { get; set; }

        public string Id { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public GameType GameType { get; set; }

        public int MinutesForPlayer { get; set; }

        private void ChangeTurns()
        {
            if (Player1.HasToMove)
            {
                Player1.HasToMove = false;
                Player2.HasToMove = true;
            }
            else
            {

                Player2.HasToMove = false;
                Player1.HasToMove = true;
            }

            var gameOverCheck = ChessBoard.CheckMateOrStalemate(MovingPlayer.Color);
            if (gameOverCheck != GameOver.None)
                OnGameOver?.Invoke(MovingPlayer, new GameOverEventArgs(gameOverCheck));
            if (ChessBoard.CheckDraw())
                OnGameOver?.Invoke(MovingPlayer, new GameOverEventArgs(GameOver.Draw));
        }
    }
}
