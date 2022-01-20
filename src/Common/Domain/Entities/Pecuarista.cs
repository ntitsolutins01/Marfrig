using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Pecuarista : AuditableEntity
    {
        public Pecuarista()
        {
            CompraGados = new List<CompraGado>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }
        
        public IList<CompraGado> CompraGados { get; set; }

    }
}
