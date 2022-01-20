using System;
using Domain.Common;

namespace Domain.Entities
{
    public class CompraGado : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Nome { get; set; }

        public int IdPecuarista { get; set; }
        public Pecuarista Pecuarista { get; set; }

    }
}
