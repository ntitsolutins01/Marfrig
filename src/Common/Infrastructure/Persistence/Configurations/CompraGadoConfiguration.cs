using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CompraGadoConfiguration : IEntityTypeConfiguration<CompraGado>
    {
        public void Configure(EntityTypeBuilder<CompraGado> builder)
        {

            builder.Property(t => t.IdPecuarista)
                .IsRequired();
        }
    }
}
