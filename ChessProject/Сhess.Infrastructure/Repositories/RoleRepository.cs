using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.Models;
using Chess.Core.Repositories;

namespace Сhess.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ChessContext _context;

        public RoleRepository(ChessContext context)
        {
            _context = context;
        }

        public Role Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> All(Func<Role, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Insert(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
