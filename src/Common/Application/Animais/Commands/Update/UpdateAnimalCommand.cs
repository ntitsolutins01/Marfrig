using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Domain.Entities;
using MapsterMapper;

namespace Application.Animais.Commands.Update
{
    public class UpdateAnimalCommand : IRequestWrapper<AnimalDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Preco { get; set; }
    }

    public class UpdateAnimalCommandHandler : IRequestHandlerWrapper<UpdateAnimalCommand, AnimalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAnimalCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AnimalDto>> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Animais.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Animal), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
                entity.Descricao = request.Name;
            entity.Preco = request.Preco;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<AnimalDto>(entity));
        }
    }
}
