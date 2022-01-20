using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Animal : AuditableEntity
    {
        public Animal()
        {
            CompraGadoItems = new List<CompraGadoItem>();
        }

        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }


        public IList<CompraGadoItem> CompraGadoItems { get; set; }

    }
}
