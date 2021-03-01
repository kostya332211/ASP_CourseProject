using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.Models;
using Chess.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Сhess.Infrastructure.Repositories
{
    public class GameHistoryRepository : IGameHistoryRepository
    {
        private readonly ChessContext _context;

        public GameHistoryRepository(ChessContext context)
        {
            _context = context;
        }

        public IEnumerable<GameHistory> All()
        {
            return _context.GameHistory
                .Include(x => x.GameType)
                .Include(x => x.WhitePlayer)
                .Include(x => x.BlackPlayer)
                .Include(x => x.EndGameType)
                .Include(x => x.Winner).ToList();
        }

        public IEnumerable<GameHistory> All(Func<GameHistory, bool> predicate)
        {
            return All().Where(predicate);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public GameHistory Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(GameHistory entity)
        {
            _context.GameHistory.Add(entity);
            _context.SaveChanges();
        }
    }
}
