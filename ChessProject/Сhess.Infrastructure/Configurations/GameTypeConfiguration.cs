using Chess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Сhess.Infrastructure.Configurations
{
    class GameTypeConfiguration : IEntityTypeConfiguration<GameType>
    {
        public void Configure(EntityTypeBuilder<GameType> builder)
        {
            builder.ToTable("GameType");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Type).HasMaxLength(64).IsRequired();
        }
    }
}
