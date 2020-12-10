using Chess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Сhess.Infrastructure.Configurations
{
    class PlayerDetailsConfiguration : IEntityTypeConfiguration<PlayerDetails>
    {
        public void Configure(EntityTypeBuilder<PlayerDetails> builder)
        {
            builder.ToTable("PlayerDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BlitzRating).HasDefaultValue(1700).IsRequired();
            builder.Property(x => x.RapidRating).HasDefaultValue(1700).IsRequired();
            builder.Property(x => x.BulletRating).HasDefaultValue(1700).IsRequired();
            builder.Property(x => x.OnlyKnightsRating).HasDefaultValue(1700).IsRequired();
            builder.Property(x => x.OnlyPawnsRating).HasDefaultValue(1700).IsRequired();
            builder.Property(x => x.OnlyQueensRating).HasDefaultValue(1700).IsRequired();
        }
    }
}
