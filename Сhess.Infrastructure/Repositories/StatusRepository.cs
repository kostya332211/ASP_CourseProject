using System;
using System.Collections.Generic;
using Chess.Core.Models;
using Chess.Core.Repositories;

namespace Сhess.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ChessContext _context;

        public StatusRepository(ChessContext context)
        {
            _context = context;
        }

        public Status Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Status> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Status> All(Func<Status, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Insert(Status entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
