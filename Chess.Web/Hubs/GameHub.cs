using Chess.Logic;
using Chess.Logic.EventArguments;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Chess.Core.Repositories;
using Chess.Web.Controllers;
using Chess.Web.Models.ChatModels;
using Chess.Web.Models.GameModels;
using Сhess.Infrastructure;
using Player = Chess.Logic.Player;
using GameType = Chess.Core.GameModels.GameType;

namespace Chess.Web.Hubs
{
    public class GameHub : Hub
    {
        private readonly Dictionary<string, Player> _players =
            new Dictionary<string, Player>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, Game> _games =
            new Dictionary<string, Game>(StringComparer.OrdinalIgnoreCase);

        private readonly List<Player> _waitingPlayersBullet = new List<Player>();

        private readonly List<Player> _waitingPlayersBlitz = new List<Player>();

        private readonly List<Player> _waitingPlayersRapid = new List<Player>();

        private readonly List<Player> _waitingPlayersOnlyPawnsMode = new List<Player>();

        private readonly List<Player> _waitingPlayersOnlyKnightsMode = new List<Player>();

        private readonly List<Player> _waitingPlayersOnlyQueensMode = new List<Player>();

        private void EndGameObject()
        {

        }

        public void Send(string message, string gameId)
        {
            var userName = "Anon";
            foreach (var claim in Context.GetHttpContext().User.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    userName = claim.Value;
                }
            }

            var game = _games.First(x => x.Key == gameId).Value;
            if (game.Player1.UserName == userName)
            {
                Clients.Caller.SendAsync("Send",
                    new MessageModel() { Name = userName, Message = message, IsCallerMessage = true });
                Clients.Client(game.Player2.Id).SendAsync("Send",
                    new MessageModel() { Name = userName, Message = message, IsCallerMessage = false });
            }
            else
            {
                Clients.Caller.SendAsync("Send",
                    new MessageModel() { Name = userName, Message = message, IsCallerMessage = true });
                Clients.Client(game.Player1.Id).SendAsync("Send",
                    new MessageModel() { Name = userName, Message = message, IsCallerMessage = false });
            }

        }

        public void CancelSearch()
        {
            var nickname = Context.GetHttpContext().User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).FirstOrDefault();
            var (_, value) = _players.FirstOrDefault(x => x.Value.UserName == nickname);
            _waitingPlayersBullet.Remove(value);
            _waitingPlayersBlitz.Remove(value);
            _waitingPlayersRapid.Remove(value);
            _waitingPlayersOnlyPawnsMode.Remove(value);
            _waitingPlayersOnlyKnightsMode.Remove(value);
            _waitingPlayersOnlyQueensMode.Remove(value);
        }

        public void OpponentTimeIsOver(string playerName, string gameId)
        {
            var player = _players[Context.ConnectionId];
            var game = GetGame(player, out Player opponent);

            var model = new EndGameModel()
            {
                User1Nickname = game.Player1.Color == Color.White ? game.Player1.UserName : game.Player2.UserName,
                User2Nickname = game.Player1.Color == Color.Black ? game.Player1.UserName : game.Player2.UserName,
                WinnerNickname = player.UserName,
                GameType = game.GameType,
                EndGameType = EndGameType.TimeIsOver
            };

            Clients.Client(player.Id).SendAsync("EndGame", model, true);

            Clients.Client(opponent.Id).SendAsync("EndGame", model, false);

        }

        public void SendDraw()
        {
            var player = _players[Context.ConnectionId];
            var game = GetGame(player, out Player opponent);

            Clients.Client(opponent.Id).SendAsync("AcceptDraw");

        }

        public void AcceptDraw()
        {
            var player = _players[Context.ConnectionId];
            var game = GetGame(player, out Player opponent);

            var model = new EndGameModel()
            {
                User1Nickname = game.Player1.Color == Color.White ? game.Player1.UserName : game.Player2.UserName,
                User2Nickname = game.Player1.Color == Color.Black ? game.Player1.UserName : game.Player2.UserName,
                WinnerNickname = null,
                GameType = game.GameType,
                EndGameType = EndGameType.DrawMutualConsent
            };

            Clients.Client(player.Id).SendAsync("EndGame", model, true);

            Clients.Client(opponent.Id).SendAsync("EndGame", model, false);

        }

        public async Task FindGame(GameType type, int rating)
        {
            var nickname = Context.GetHttpContext().User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).FirstOrDefault();

            var joiningPlayer = new Player(nickname, Context.ConnectionId) {Rating = rating};
            _players[joiningPlayer.Id] = joiningPlayer;

            await Clients.Caller.SendAsync("PlayerJoined", joiningPlayer);

            var opponent = type switch
            {
                GameType.Bullet => _waitingPlayersBullet.FirstOrDefault(),
                GameType.Blitz => _waitingPlayersBlitz.FirstOrDefault(),
                GameType.Rapid => _waitingPlayersRapid.FirstOrDefault(),
                GameType.OnlyPawnsMode => _waitingPlayersOnlyPawnsMode.FirstOrDefault(),
                GameType.OnlyKnightsMode => _waitingPlayersOnlyKnightsMode.FirstOrDefault(),
                GameType.OnlyQueensMode => _waitingPlayersOnlyQueensMode.FirstOrDefault(),
                _ => null
            };

            if (opponent == null)
            {
                switch (type)
                {
                    case GameType.Bullet:
                        _waitingPlayersBullet.Add(joiningPlayer);
                        break;
                    case GameType.Blitz:
                        _waitingPlayersBlitz.Add(joiningPlayer);
                        break;
                    case GameType.Rapid:
                        _waitingPlayersRapid.Add(joiningPlayer);
                        break;
                    case GameType.OnlyPawnsMode:
                        _waitingPlayersOnlyPawnsMode.Add(joiningPlayer);
                        break;
                    case GameType.OnlyKnightsMode:
                        _waitingPlayersOnlyKnightsMode.Add(joiningPlayer);
                        break;
                    case GameType.OnlyQueensMode:
                        _waitingPlayersOnlyQueensMode.Add(joiningPlayer);
                        break;
                }

            }
            else
            {
                joiningPlayer.Color = Color.Black;
                opponent.Color = Color.White;
                var game = new Game(opponent, joiningPlayer, type);
                game.OnGameOver += Game_OnGameOver;
                _games[game.Id] = game;

                await Task.WhenAll(Groups.AddToGroupAsync(game.Player1.Id, groupName: game.Id),
                    Groups.AddToGroupAsync(game.Player2.Id, groupName: game.Id),
                    Clients.Group(game.Id).SendAsync("Start", game));
            }
        }

        private Game GetGame(Player player, out Player opponent)
        {
            opponent = null;
            var foundGame = _games.Values.FirstOrDefault(g => g.Id == player.GameId);

            if (foundGame == null)
                return null;

            opponent = (player.Id == foundGame.Player1.Id) ? foundGame.Player2 : foundGame.Player1;
            return foundGame;
        }

        private void RemoveGame(string gameId)
        {
            if (!_games.Remove(gameId, out var foundGame))
                return;

            _players.Remove(foundGame.Player1.Id);
            _players.Remove(foundGame.Player2.Id);
        }

        private void Game_OnGameOver(object sender, EventArgs e)
        {
            var player = sender as Player;
            var game = GetGame(player, out Player opponent);

            var eventArgs = e as GameOverEventArgs;

            var model = new EndGameModel()
            {
                User1Nickname = game.Player1.Color == Color.White ? game.Player1.UserName : game.Player2.UserName,
                User2Nickname = game.Player1.Color == Color.Black ? game.Player1.UserName : game.Player2.UserName,
                WinnerNickname = opponent.UserName,
                GameType = game.GameType,
                EndGameType = EndGameType.LeaveOpponent
            };

            switch (eventArgs.GameOver)
            {
                case GameOver.None:
                    break;
                case GameOver.Checkmate:
                    model.EndGameType = EndGameType.Checkmate;
                    break;
                case GameOver.Stalemate:
                    model.EndGameType = EndGameType.Checkmate;
                    break; 
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Clients.Client(player.Id).SendAsync("EndGame", model, false);

            Clients.Client(opponent.Id).SendAsync("EndGame", model, true);

        }

        public async Task MoveSelected(string a, string b)
        {
            var player = _players[Context.ConnectionId];
            if (!player.HasToMove)
                return;
                
            var game = GetGame(player, out Player opponent);
            var start = game.ChessBoard.GetSquareAtPosition(a);
            var end = game.ChessBoard.GetSquareAtPosition(b);
            var move = game.MoveSelected(start, end);
            await Clients.All.SendAsync("MoveDone", game, move);
        }

        public async Task PieceSelected(int x, int y)
        {
            var player = _players[Context.ConnectionId];
            if (!player.HasToMove)
                return;

            var game = GetGame(player, out Player opponent);
            var square = game.ChessBoard.GetSquareAtPosition(new Position(x, y));
            game.ChessBoard.ClearBoardSelections();
            game.ChessBoard.CalculatePossibleMovesForPiece(square.Piece);
            square.IsSelected = true;
            await Clients.All.SendAsync("ShowPossibleMoves", game);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _players.TryGetValue(Context.ConnectionId, out var leavingPlayer);

            if (leavingPlayer != null)
            {
                Game game = GetGame(leavingPlayer, out var opponent);
                

                if (game != null)
                {
                    var model = new EndGameModel()
                    {
                        User1Nickname = game.Player1.UserName,
                        User2Nickname = game.Player2.UserName,
                        WinnerNickname = opponent.UserName,
                        GameType = game.GameType,
                        EndGameType = EndGameType.LeaveOpponent
                    };

                    Clients.Client(opponent.Id).SendAsync("EndGame", model, true);


                    RemoveGame(game.Id);
                }
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
