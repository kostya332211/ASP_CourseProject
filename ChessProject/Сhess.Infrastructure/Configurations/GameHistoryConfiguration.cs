using Chess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Сhess.Infrastructure.Configurations
{
    class GameHistoryConfiguration : IEntityTypeConfiguration<GameHistory>
    {
        public void Configure(EntityTypeBuilder<GameHistory> builder)
        {
            builder.ToTable("GameHistory");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BlackPlayerId).IsRequired();
            builder.Property(x => x.GameDate).IsRequired();
            builder.Property(x => x.WhitePlayerId).IsRequired();
            builder.Property(x => x.EndGameTypeId).IsRequired();
            builder.Property(x => x.GameTypeId).IsRequired();
        }
    }
}
