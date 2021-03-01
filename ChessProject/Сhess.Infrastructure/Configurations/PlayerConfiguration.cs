using Chess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Сhess.Infrastructure.Configurations
{
    class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).HasMaxLength(128).IsRequired();
            builder.HasIndex(x => x.Nickname).IsUnique();
            builder.Property(x => x.Nickname).HasMaxLength(128).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.PasswordSalt).HasMaxLength(64).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(512).IsRequired();
        }
    }
}
