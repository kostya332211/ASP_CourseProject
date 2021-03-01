using Chess.Logic.EventArguments;
using System;
using System.Threading;

namespace Chess.Logic
{
    public class Player
    {

        public string Id { get; set; }

        public string GameId { get; set; }

        public string UserName { get; set; }

        public Color Color { get; set; }

        public int Rating { get; set; }

        public bool HasToMove { get; set; }

        public Player(Color color)
        {
            Color = color;
            InitializeMove();
        }

        public Player(string userName, string connectionId)
        {
            UserName = userName;
            Id = connectionId;
            InitializeMove();
        }


        public void InitializeMove()
        {
            if (Color == Color.White)
                HasToMove = true;
        }
    }
}
