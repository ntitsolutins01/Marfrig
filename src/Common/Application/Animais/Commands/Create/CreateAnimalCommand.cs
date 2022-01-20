using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Domain.Entities;
using MapsterMapper;

namespace Application.Animais.Commands.Create
{
    public class CreateAnimalCommand : IRequestWrapper<AnimalDto>
    {
        public string Name { get; set; }
    }

    public class CreateAnimalCommandHandler : IRequestHandlerWrapper<CreateAnimalCommand, AnimalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAnimalCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AnimalDto>> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
        {
            var entity = new Animal
            {
                Descricao = request.Name
            };


            await _context.Animais.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<AnimalDto>(entity));
        }
    }
}
