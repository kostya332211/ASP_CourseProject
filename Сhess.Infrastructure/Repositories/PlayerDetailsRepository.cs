using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.Models;
using Chess.Core.Repositories;

namespace Сhess.Infrastructure.Repositories
{
    public class PlayerDetailsRepository : IPlayerDetailsRepository
    {
        private readonly ChessContext _context;

        public PlayerDetailsRepository(ChessContext context)
        {
            _context = context;
        }

        public PlayerDetails Get(Guid id)
        {
            return _context.PlayerDetails.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PlayerDetails> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerDetails> All(Func<PlayerDetails, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Insert(PlayerDetails entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
