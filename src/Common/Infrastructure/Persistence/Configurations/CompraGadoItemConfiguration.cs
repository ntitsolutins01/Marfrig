using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CompraGadoItemConfiguration : IEntityTypeConfiguration<CompraGadoItem>
    {
        public void Configure(EntityTypeBuilder<CompraGadoItem> builder)
        {

            builder.Property(t => t.IdAnimal)
                .IsRequired();

            builder.Property(t => t.IdCompraGado)
                .IsRequired();
        }
    }
}
