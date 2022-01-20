using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class CompraGadoItem : AuditableEntity
    {
        public CompraGadoItem()
        {
            CompraGados = new List<CompraGado>();
        }

        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int IdCompraGado { get; set; }
        public IList<CompraGado> CompraGados { get; set; }
        
        public int IdAnimal { get; set; }
        public Animal Animal { get; set; }

    }
}
