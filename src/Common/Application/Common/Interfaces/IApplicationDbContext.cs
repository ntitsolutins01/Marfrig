using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<CompraGadoItem> CompraGadoItems { get; set; }
        DbSet<Pecuarista> Pecuaristas { get; set; }
        DbSet<Animal> Animais { get; set; }
        DbSet<CompraGado> CompraGados { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
