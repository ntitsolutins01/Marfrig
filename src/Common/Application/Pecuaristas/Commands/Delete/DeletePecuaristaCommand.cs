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

namespace Application.Pecuaristas.Commands.Delete
{
    public class DeletePecuaristaCommand : IRequestWrapper<PecuaristaDto>
    {
        public int Id { get; set; }
    }

    public class DeletePecuaristaCommandHandler : IRequestHandlerWrapper<DeletePecuaristaCommand, PecuaristaDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeletePecuaristaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PecuaristaDto>> Handle(DeletePecuaristaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Pecuaristas
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Pecuarista), request.Id);
            }

            _context.Pecuaristas.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<PecuaristaDto>(entity));
        }
    }
}
