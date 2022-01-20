using System.Collections.Generic;
using Domain.Entities;
using Mapster;

namespace Application.Dto
{
    public class CompraGadoItemDto : IRegister 
    {
        public CompraGadoItemDto()
        {
            CompraGados = new List<CompraGadoDto>();
        }

        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int IdCompraGado { get; set; }
        public IList<CompraGadoDto> CompraGados { get; set; }
        
        public int IdAnimal { get; set; }
        public AnimalDto Animal { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CompraGadoItem, CompraGadoItemDto>()
            .Map(dest => dest.Quantidade,
                src => $"{src.CreateDate.ToShortDateString()}");
        }
    }
}
