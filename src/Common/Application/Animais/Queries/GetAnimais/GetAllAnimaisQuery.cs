using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Application.Animais.Queries.GetAnimais;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Animais.Queries.GetAnimais
{
    public class GetAllAnimaisQuery : IRequestWrapper<List<AnimalDto>>
    {

    }

    public class GetAnimaisQueryHandler : IRequestHandlerWrapper<GetAllAnimaisQuery, List<AnimalDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAnimaisQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<AnimalDto>>> Handle(GetAllAnimaisQuery request, CancellationToken cancellationToken)
        {
            List<AnimalDto> list = await _context.Animais
                .Include(x => x.CompraGadoItems)
                .ProjectToType<AnimalDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<AnimalDto>>(ServiceError.NotFount);
        }
    }
}
