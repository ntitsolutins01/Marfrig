using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PecuaristaConfiguration : IEntityTypeConfiguration<Pecuarista>
    {
        public void Configure(EntityTypeBuilder<Pecuarista> builder)
        {
            builder.Property(t => t.Nome)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
