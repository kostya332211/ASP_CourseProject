using System.Collections.Generic;
using Chess.Logic.Pieces;

namespace Chess.Logic.Positions
{
    public class PositionsContainer
    {
        private static readonly Dictionary<string, Piece> DefaultPosition = new Dictionary<string, Piece>()
        {
            { "A1", new Rook(Color.White) },  { "B1", new Knight(Color.White) }, { "C1", new Bishop(Color.White) }, { "D1", new Queen(Color.White) },
            { "E1", new King(Color.White) },  { "F1", new Bishop(Color.White) }, { "G1", new Knight(Color.White) }, { "H1", new Rook(Color.White) },
            { "A2", new Pawn(Color.White) },  { "B2", new Pawn(Color.White) },   { "C2", new Pawn(Color.White) },   { "D2", new Pawn(Color.White) },
            { "E2", new Pawn(Color.White) },  { "F2", new Pawn(Color.White) },   { "G2", new Pawn(Color.White) },   { "H2", new Pawn(Color.White) },

            { "A7", new Pawn(Color.Black) },  { "B7", new Pawn(Color.Black) },   { "C7", new Pawn(Color.Black) },   { "D7", new Pawn(Color.Black) },
            { "E7", new Pawn(Color.Black) },  { "F7", new Pawn(Color.Black) },   { "G7", new Pawn(Color.Black) },   { "H7", new Pawn(Color.Black) },
            { "A8", new Rook(Color.Black) },  { "B8", new Knight(Color.Black) }, { "C8", new Bishop(Color.Black) }, { "D8", new Queen(Color.Black) },
            { "E8", new King(Color.Black) },  { "F8", new Bishop(Color.Black) }, { "G8", new Knight(Color.Black) }, { "H8", new Rook(Color.Black) }
        };

        private static readonly Dictionary<string, Piece> OnlyPawnsPosition = new Dictionary<string, Piece>()
        {
            { "E1", new King(Color.White) },
            { "A2", new Pawn(Color.White) },  { "B2", new Pawn(Color.White) },   { "C2", new Pawn(Color.White) },   { "D2", new Pawn(Color.White) },
            { "E2", new Pawn(Color.White) },  { "F2", new Pawn(Color.White) },   { "G2", new Pawn(Color.White) },   { "H2", new Pawn(Color.White) },

            { "A7", new Pawn(Color.Black) },  { "B7", new Pawn(Color.Black) },   { "C7", new Pawn(Color.Black) },   { "D7", new Pawn(Color.Black) },
            { "E7", new Pawn(Color.Black) },  { "F7", new Pawn(Color.Black) },   { "G7", new Pawn(Color.Black) },   { "H7", new Pawn(Color.Black) },
            { "E8", new King(Color.Black) }
        };

        private static readonly Dictionary<string, Piece> OnlyKnightsPosition = new Dictionary<string, Piece>()
        {
            { "A1", new Knight(Color.White) },  { "B1", new Knight(Color.White) }, { "C1", new Knight(Color.White) }, { "D1", new Knight(Color.White) },
            { "E1", new King(Color.White) },    { "F1", new Knight(Color.White) }, { "G1", new Knight(Color.White) }, { "H1", new Knight(Color.White) },
            { "A2", new Knight(Color.White) },  { "B2", new Knight(Color.White) }, { "C2", new Knight(Color.White) }, { "D2", new Knight(Color.White) },
            { "E2", new Knight(Color.White) },  { "F2", new Knight(Color.White) }, { "G2", new Knight(Color.White) }, { "H2", new Knight(Color.White) },

            { "A7", new Knight(Color.Black) },  { "B7", new Knight(Color.Black) }, { "C7", new Knight(Color.Black) }, { "D7", new Knight(Color.Black) },
            { "E7", new Knight(Color.Black) },  { "F7", new Knight(Color.Black) }, { "G7", new Knight(Color.Black) }, { "H7", new Knight(Color.Black) },
            { "A8", new Knight(Color.Black) },  { "B8", new Knight(Color.Black) }, { "C8", new Knight(Color.Black) }, { "D8", new Knight(Color.Black) },
            { "E8", new King(Color.Black) },    { "F8", new Knight(Color.Black) }, { "G8", new Knight(Color.Black) }, { "H8", new Knight(Color.Black) }
        };

        private static readonly Dictionary<string, Piece> OnlyQueensPosition = new Dictionary<string, Piece>()
        {
            { "A1", new Queen(Color.White) },  { "B1", new Queen(Color.White) }, { "C1", new Queen(Color.White) }, { "D1", new Queen(Color.White) },
            { "E1", new King(Color.White) },   { "F1", new Queen(Color.White) }, { "G1", new Queen(Color.White) }, { "H1", new Queen(Color.White) },
            { "A2", new Queen(Color.White) },  { "B2", new Queen(Color.White) }, { "C2", new Queen(Color.White) }, { "D2", new Queen(Color.White) },
            { "E2", new Queen(Color.White) },  { "F2", new Queen(Color.White) }, { "G2", new Queen(Color.White) }, { "H2", new Queen(Color.White) },

            { "A7", new Queen(Color.Black) },  { "B7", new Queen(Color.Black) }, { "C7", new Queen(Color.Black) }, { "D7", new Queen(Color.Black) },
            { "E7", new Queen(Color.Black) },  { "F7", new Queen(Color.Black) }, { "G7", new Queen(Color.Black) }, { "H7", new Queen(Color.Black) },
            { "A8", new Queen(Color.Black) },  { "B8", new Queen(Color.Black) }, { "C8", new Queen(Color.Black) }, { "D8", new Queen(Color.Black) },
            { "E8", new King(Color.Black) },   { "F8", new Queen(Color.Black) }, { "G8", new Queen(Color.Black) }, { "H8", new Queen(Color.Black) }
        };

        public static Dictionary<string, Piece> GetStartPosition(StartPosition position)
        {
            return position switch
            {
                StartPosition.DefaultPosition => DefaultPosition,
                StartPosition.OnlyKnightsPosition => OnlyKnightsPosition,
                StartPosition.OnlyPawnsPosition => OnlyPawnsPosition,
                StartPosition.OnlyQueensPosition => OnlyQueensPosition,
                _ => DefaultPosition
            };
        }
    }
}
