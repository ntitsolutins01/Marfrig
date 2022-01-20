using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Domain.Entities;
using MapsterMapper;

namespace Application.Pecuaristas.Commands.Update
{
    public class UpdatePecuaristaCommand : IRequestWrapper<PecuaristaDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }

    public class UpdatePecuaristaCommandHandler : IRequestHandlerWrapper<UpdatePecuaristaCommand, PecuaristaDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePecuaristaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PecuaristaDto>> Handle(UpdatePecuaristaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Pecuaristas.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Pecuarista), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
                entity.Nome = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<PecuaristaDto>(entity));
        }
    }
}
