using System;
using System.Collections.Generic;
using Chess.Core.Models;
using Chess.Core.Repositories;

namespace Сhess.Infrastructure.Repositories
{
    public class GameTypeRepository : IGameTypeRepository
    {
        private readonly ChessContext _context;

        public GameTypeRepository(ChessContext context)
        {
            _context = context;
        }

        public GameType Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameType> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameType> All(Func<GameType, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Insert(GameType entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
