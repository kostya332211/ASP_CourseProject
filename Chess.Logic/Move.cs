using Chess.Logic.Pieces;
using System;
using System.Linq;

namespace Chess.Logic
{
    public class Move : ICloneable
    {
        private readonly Board _board;
        private readonly MoveType _moveType;

        public Move(Board board, Square start, Square end, MoveType moveType)
        {
            Start = start;
            End = end;
            _board = board;
            _moveType = moveType;
        }

        public Move()
        {

        }

        public event EventHandler OnPieceCaputured;

        public Square Start { get; set; }

        public Square End { get; set; }

        public string Do()
        {
            Piece lostPiece = null;
            //En passant
            if (_moveType == MoveType.EnPassant)
            {
                _board.ShiftPiece(Start, End);
                var square = End.Color == Color.White ?
                            _board.GetSquareAtPosition(End.Position + new Position(0, 1)) :
                            _board.GetSquareAtPosition(End.Position + new Position(0, -1));

                OnPieceCaputured?.Invoke(square.Piece, new EventArgs());
                lostPiece = square.Piece;
                square.Piece = null;
            }

            //Castle
            if (_moveType == MoveType.Castle)
            {
                var dir = Start.Position.GetDirection(End.Position);
                var rooks = _board.GetAllSquaresWithPieces(Start.Piece.Color).Where(x => x.Piece is Rook);
                _board.ShiftPiece(Start, End);
                _board.ShiftPiece(rooks.FirstOrDefault(x => End.Position.IsInDirection(dir, x.Position)),
                    _board.GetSquareAtPosition(End.Position + new Position(dir.PosX * -1, dir.PosY)));
            }

            //Standard
            if (_moveType == MoveType.Normal)
            {
                lostPiece = _board.ShiftPiece(Start, End);
                if (lostPiece != null)
                    OnPieceCaputured?.Invoke(lostPiece, new EventArgs());
            }

            End.Piece.NumberOfMoves += 1;

            return lostPiece?.GetType().Name;
        }

        public void Undo()
        {

            Start.Piece.NumberOfMoves -= 1;
        }

        public Board Predict()
        {
            var predictionBoard = _board.Clone() as Board;
            predictionBoard.ShiftPiece(predictionBoard.Squares.FirstOrDefault(x => x.Position == Start.Position), predictionBoard.Squares.FirstOrDefault(x => x.Position == End.Position));
            return predictionBoard;
        }

        public override string ToString()
        {
            return Start.ToString() + "->" + End.ToString();
        }

        public object Clone()
        {
            return new Move()
            {
                Start = Start.Clone() as Square,
                End = End.Clone() as Square
            };
        }
    }

    public enum MoveType
    {
        Normal,
        Castle,
        EnPassant
    }
}
