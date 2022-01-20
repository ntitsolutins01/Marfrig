using System.Collections.Generic;
using Domain.Entities;
using Mapster;

namespace Application.Dto
{
    public class PecuaristaDto : IRegister
    {
        public PecuaristaDto()
        {
            CompraGados = new List<CompraGadoDto>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }
        
        public IList<CompraGadoDto> CompraGados { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Pecuarista, PecuaristaDto>()
                .Map(dest => dest.Nome, src => "Sig. " + src.Nome, srcCond => srcCond.Nome == "Karacabey")
                .Map(dest => dest.Nome, src => "Sr. " + src.Nome, srcCond => srcCond.Nome == "Osmangazi")
                .Map(dest => dest.Nome, src => src.Nome);
        }
    }
}
