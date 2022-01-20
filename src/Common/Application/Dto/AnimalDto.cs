using System.Collections.Generic;
using Domain.Entities;
using Mapster;

namespace Application.Dto
{
    public class AnimalDto : IRegister
    {
        public AnimalDto()
        {
            CompraGadoItems = new List<CompraGadoItem>();
        }

        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }

        public IList<CompraGadoItem> CompraGadoItems { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Animal, AnimalDto>()
                .Map(dest => dest.Descricao, src => "Boi " + src.Descricao, srcCond => srcCond.Descricao == "Nelore")
                .Map(dest => dest.Descricao, src => "Boi " + src.Descricao, srcCond => srcCond.Descricao == "Wagyo")
                .Map(dest => dest.Descricao, src => src.Descricao);
        }
    }
}
