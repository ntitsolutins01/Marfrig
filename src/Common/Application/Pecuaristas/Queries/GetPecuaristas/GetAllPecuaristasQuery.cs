using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Pecuaristas.Queries.GetPecuaristas
{
    public class GetAllPecuaristasQuery : IRequestWrapper<List<CompraGadoItemDto>>
    {

    }

    public class GetCitiesQueryHandler : IRequestHandlerWrapper<GetAllPecuaristasQuery, List<CompraGadoItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<CompraGadoItemDto>>> Handle(GetAllPecuaristasQuery request, CancellationToken cancellationToken)
        {
            List<CompraGadoItemDto> list = await _context.Pecuaristas
                .Include(x => x.CompraGados)
                .ProjectToType<CompraGadoItemDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<CompraGadoItemDto>>(ServiceError.NotFount);
        }
    }
}
