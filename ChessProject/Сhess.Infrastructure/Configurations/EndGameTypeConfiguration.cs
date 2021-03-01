using Chess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Сhess.Infrastructure.Configurations
{
    class EndGameTypeConfiguration : IEntityTypeConfiguration<EndGameType>
    {
        public void Configure(EntityTypeBuilder<EndGameType> builder)
        {
            builder.ToTable("EndGameType");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Type).HasMaxLength(64).IsRequired();
        }
    }
}
