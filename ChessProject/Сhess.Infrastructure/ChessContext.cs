using Microsoft.EntityFrameworkCore;
using Chess.Core.Models;
using Сhess.Infrastructure.Configurations;

namespace Сhess.Infrastructure
{
    public class ChessContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerDetails> PlayerDetails { get; set; }

        public DbSet<GameHistory> GameHistory { get; set; }

        public DbSet<EndGameType> EndGameTypes { get; set; }

        public DbSet<GameType> GameTypes { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Role> Roles { get; set; }

        public ChessContext(DbContextOptions<ChessContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());
            builder.ApplyConfiguration(new EndGameTypeConfiguration());
            builder.ApplyConfiguration(new GameTypeConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new PlayerDetailsConfiguration());
            builder.ApplyConfiguration(new GameHistoryConfiguration());
        }
    }
}
