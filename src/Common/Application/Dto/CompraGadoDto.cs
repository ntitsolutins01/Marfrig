using System;

namespace Application.Dto
{
    public class CompraGadoDto
    {
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Nome { get; set; }

        public int IdPecuarista { get; set; }
        public PecuaristaDto Pecuarista { get; set; }
    }
}
