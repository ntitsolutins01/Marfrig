using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Pecuaristas.Queries.GetPecuaristaById
{
    public class GetPecuaristaByIdQuery : IRequestWrapper<PecuaristaDto>
    {
        public int PecuaristaId { get; set; }
    }

    public class GetPecuaristaByIdQueryHandler : IRequestHandlerWrapper<GetPecuaristaByIdQuery, PecuaristaDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPecuaristaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PecuaristaDto>> Handle(GetPecuaristaByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await _context.Pecuaristas
                .Where(x => x.Id == request.PecuaristaId)
                .Include(d => d.CompraGados)
                .ProjectToType<PecuaristaDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return city != null ? ServiceResult.Success(city) : ServiceResult.Failed<PecuaristaDto>(ServiceError.NotFount);
        }
    }
}
