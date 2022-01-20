using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Domain.Entities;
using MapsterMapper;

namespace Application.Pecuaristas.Commands.Create
{
    public class CreatePecuaristaCommand : IRequestWrapper<PecuaristaDto>
    {
        public string Name { get; set; }
    }

    public class CreatePecuaristaCommandHandler : IRequestHandlerWrapper<CreatePecuaristaCommand, PecuaristaDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePecuaristaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PecuaristaDto>> Handle(CreatePecuaristaCommand request, CancellationToken cancellationToken)
        {
            var entity = new Pecuarista
            {
                Nome = request.Name
            };


            await _context.Pecuaristas.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<PecuaristaDto>(entity));
        }
    }
}
