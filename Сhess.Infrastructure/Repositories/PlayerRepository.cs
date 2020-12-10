using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.Models;
using Chess.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Сhess.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ChessContext _context;

        public PlayerRepository(ChessContext context)
        {
            _context = context;
        }

        public Player FindByEmail(string email)
        {
            return _context.Players.Include(x => x.Role).Include(x => x.Status).FirstOrDefault(x => x.Email == email);
        }

        public Player FindByNickname(string nickname)
        {
            return _context.Players.Include(x => x.PlayerDetails)
                .Include(x => x.Role)
                .Include(x => x.Status).FirstOrDefault(x => x.Nickname == nickname);
        }

        public IEnumerable<Player> All()
        {
            return _context.Players.Include(x => x.PlayerDetails)
                .Include(x => x.Role)
                .Include(x => x.Status)
                .ToList();
        }

        public IEnumerable<Player> All(Func<Player, bool> predicate)
        {
            return All().Where(predicate);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            _context.Players.Remove(Get(id));
        }

        public Player Get(Guid id)
        {
            return _context.Players.Include(x => x.PlayerDetails).Include(x => x.Role).Include(x => x.Status)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Player entity)
        {
            _context.Players. Add(entity);
        }

    }
}
