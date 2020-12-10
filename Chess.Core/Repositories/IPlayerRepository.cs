using System;
using Chess.Core.Models;

namespace Chess.Core.Repositories
{
    public interface IPlayerRepository : IRepository<Player, Guid>
    {
        public Player FindByEmail(string email);

        public Player FindByNickname(string nickname);

    }
}
