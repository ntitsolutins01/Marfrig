using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Animais.Commands.Delete
{
    public class DeleteAnimalCommand : IRequestWrapper<AnimalDto>
    {
        public int Id { get; set; }
    }

    public class DeleteCityCommandHandler : IRequestHandlerWrapper<DeleteAnimalCommand, AnimalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteCityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AnimalDto>> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Animais
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Animal), request.Id);
            }

            _context.Animais.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<AnimalDto>(entity));
        }
    }
}
