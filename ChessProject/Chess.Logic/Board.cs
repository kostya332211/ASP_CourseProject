using Chess.Logic.EventArguments;
using Chess.Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.GameModels;
using Chess.Logic.Positions;

namespace Chess.Logic
{
    public class Board : ICloneable
    {
        private readonly string[] _letters = { "A", "B", "C", "D", "E", "F", "G", "H" };

        public List<Square> Squares { get; set; }

        private readonly List<Move> _currentPossibleMoves;

        public Board()
        {
            Squares = new List<Square>();
            _currentPossibleMoves = new List<Move>();
        }

        public void MakeBoard(GameType type)
        {
            var toggle = Color.White;
            Squares = new List<Square>();
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var name = _letters[x] + (8 - y);
                    var square = new Square()
                    {
                        Position = new Position(x, 7 - y),
                        Color = toggle,
                        Name = name
                    };

                    square.Piece = type switch
                    {
                        GameType.OnlyKnightsMode => PositionsContainer.GetStartPosition(StartPosition.OnlyKnightsPosition)
                                                       .FirstOrDefault(keyValuePair => keyValuePair.Key.Equals(name)).Value,
                        GameType.OnlyPawnsMode => PositionsContainer.GetStartPosition(StartPosition.OnlyPawnsPosition)
                                                        .FirstOrDefault(keyValuePair => keyValuePair.Key.Equals(name)).Value,
                        GameType.OnlyQueensMode => PositionsContainer.GetStartPosition(StartPosition.OnlyQueensPosition)
                                                        .FirstOrDefault(keyValuePair => keyValuePair.Key.Equals(name)).Value,
                        _ => PositionsContainer.GetStartPosition(StartPosition.DefaultPosition)
                                                        .FirstOrDefault(keyValuePair => keyValuePair.Key.Equals(name)).Value,
                    };

                    if (x != 7)
                        toggle = toggle == Color.White ? Color.Black : Color.White;

                    Squares.Add(square);
                }
            }
        }

        private Square GetKingSquare(Color color)
        {
           return Squares.FirstOrDefault(x => x.Piece is King && x.Piece?.Color == color);
        }

        private bool IsKingChecked(Color color, Move move)
        {
            var prediction = move.Predict();
            var king = prediction.GetKingSquare(color);
            var allEnemySquares = prediction.GetAllSquaresWithPieces(Switch(color));

            if (king == null)
                return false;

            foreach (var enemySquare in allEnemySquares)
                if (enemySquare.Piece.CanBeMovedTo(king, prediction))
                    return true;

            return false;
        }

        private Square IsKingChecked(Color color)
        {
            var enemySquares = GetAllSquaresWithPieces(Switch(color));
            var king = GetKingSquare(color);
            if (king == null)
                return null;

            foreach (var enemySquare in enemySquares)
                if (enemySquare.Piece.CanBeMovedTo(king, this))
                    return enemySquare;

            return null;
        }

        private static Color Switch(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        public IEnumerable<Square> GetAllSquaresWithPieces(Color color)
        {
            return Squares.Where(square => square.Piece?.Color == color);
        }

        public IEnumerable<Move> CalculatePossibleMoves(Color color, Square start)
        {
            var list = new List<Move>();
            foreach (var square in Squares)
            {
                if (square.Position == start.Position)
                    continue;

                var passantMove = CheckEnPassant(start, square);
                if (passantMove != null)
                    list.Add(passantMove);

                var castle = CheckCastle(start, square);
                if (castle != null)
                    list.Add(castle);

                if (start.Piece.CanBeMovedTo(square, this))
                {
                    var move = new Move(this, start, square, MoveType.Normal);
                    if (!IsKingChecked(color, move))
                        list.Add(move);
                }
            }

            return list;
        }

        private Move CheckEnPassant(Square start, Square end)
        {
            Square square = null;
            switch (start.Piece.Color)
            {
                case Color.White when start.Position.PosY == 4 && (end.Position == start.Position + new Position(1, 1) && GetSquareAtPosition(start.Position + new Position(1, 0))?.Piece?.Color == Color.Black) ^
                    (end.Position == start.Position + new Position(-1, 1) && GetSquareAtPosition(start.Position + new Position(-1, 0))?.Piece?.Color == Color.Black):
                case Color.Black when start.Position.PosY == 3 && (end.Position == start.Position + new Position(1, -1) && GetSquareAtPosition(start.Position + new Position(1, 0))?.Piece?.Color == Color.White) ^
                    (end.Position == start.Position + new Position(-1, -1) && GetSquareAtPosition(start.Position + new Position(-1, 0))?.Piece?.Color == Color.White):
                    square = end;
                    break;
            }

            if (square != null)
            {
                var move = new Move(this, start, square, MoveType.EnPassant);
                if (!IsKingChecked(start.Color, move))
                    return move;
            }

            return null;
        }

        public Square GetSquareAtPosition(Position position)
        {
            return Squares.FirstOrDefault(x => x.Position == position);
        }

        public Square GetSquareAtPosition(string name)
        {
            return Squares.FirstOrDefault(x => x.Name == name);
        }

        private Move CheckCastle(Square start, Square end)
        {
            if (!(start.Piece is King)) return null;

            var rooks = GetAllSquaresWithPieces(start.Color).Where(x => x.Piece is Rook && x.Piece.NumberOfMoves == 0);

            if (rooks.Count() == 0 || start.Piece.NumberOfMoves != 0 || IsKingChecked(start.Color) != null)
                return null;

            foreach (var rook in rooks)
            {
                var squares = GetAllSquaresBetween(start.Position, rook);
                var pieces = squares.Where(x => x.Piece != null);
                squares = squares.Take(2);
                if (pieces.Count() != 0)
                    continue;

                if (squares.LastOrDefault()?.Position == end.Position)
                {
                    foreach (var m in squares)
                    {
                        var move = new Move(this, start, m, MoveType.Normal);
                        if (IsKingChecked(start.Color, move))
                            return null;
                    }

                    return new Move(this, start, end, MoveType.Castle);
                }
            }

            return null;
        }


        public GameOver CheckMateOrStalemate(Color color)
        {
            var king = GetKingSquare(color);
            var enemyPiece = IsKingChecked(color);
            var moves = CalculatePossibleMovesForPiece(king.Piece);
            if (enemyPiece != null)
            {
                var enemyPieces = GetAllSquaresWithPieces(color).Where(x => !(x.Piece is King));

                //Can be blocked?
                foreach (var piece in enemyPieces)
                {
                    var pieceMoves = king.Position.AllMovesFromStartToEnd(enemyPiece.Position, king.Position.GetDirection(enemyPiece.Position));
                    foreach (var end in pieceMoves)
                    {
                        var square = Squares.FirstOrDefault(x => x.Position == end);
                        if (piece.Piece.CanBeMovedTo(square, this))
                            return GameOver.None;
                    }
                }

                //Can be moved?
                if (moves.Count() != 0)
                    return GameOver.None;

                return GameOver.Checkmate;
            }
            else
            {
                foreach (var piece in GetAllSquaresWithPieces(color))
                    if (CalculatePossibleMovesForPiece(piece.Piece).Count() != 0)
                        return GameOver.None;

                return GameOver.Stalemate;
            }
        }

        public bool CheckDraw()
        {
            return Squares.Where(x => x.Piece != null).Count() == 2;
        }

        public IEnumerable<Square> GetAllSquaresBetween(Position a, Square b)
        {
            var start = a.Clone() as Position;
            var dir = start.GetDirection(b.Position);
            if (dir == null) yield break;

            while (start != b.Position)
            {
                start += dir;

                if (start == b.Position)
                    yield break;

                yield return GetSquareAtPosition(start);
            }
        }

        public Move MakeMove(Square start, Square end)
        {
            Move moveName = null;
            var move = _currentPossibleMoves.FirstOrDefault(x => x.End.Position == end.Position && x.Start.Position == start.Position);
            if (move == null)
                return null;
            moveName = move.Clone() as Move;
            move.Do();
            ClearBoardSelections();
            return moveName;
        }

        public List<Move> CalculatePossibleMovesForPiece(Piece piece)
        {
            var list = new List<Move>();
            foreach (var move in CalculatePossibleMoves(piece.Color, Squares.FirstOrDefault(x => x.Position == piece.Position)))
            {
                move.End.PossibleMove = true;
                _currentPossibleMoves.Add(move);
                list.Add(move);
            }

            return list;
        }

        public void ClearBoardSelections()
        {
            _currentPossibleMoves.Clear();
            foreach (var square in Squares)
            {
                square.PossibleMove = false;
                square.IsSelected = false;
            }
        }

        public Piece ShiftPiece(Square start, Square end)
        {
            var startPiece = start.Piece.Clone() as Piece;
            var endPiece = end.Piece?.Clone() as Piece;

            start.Piece = null;
            end.Piece = startPiece;

            return endPiece;
        }

        public object Clone()
        {
            var board = new Board();

            foreach (var square in Squares)
                board.Squares.Add(square.Clone() as Square);

            return board;
        }
    }
}
