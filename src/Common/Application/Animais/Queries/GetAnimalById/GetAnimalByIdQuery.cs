using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Animais.Queries.GetAnimalById
{
    public class GetAnimalByIdQuery : IRequestWrapper<AnimalDto>
    {
        public int AnimalId { get; set; }
    }

    public class GetAnimalByIdQueryHandler : IRequestHandlerWrapper<GetAnimalByIdQuery, AnimalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAnimalByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AnimalDto>> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Animais
                .Where(x => x.Id == request.AnimalId)
                .Include(d => d.CompraGadoItems)
                .ProjectToType<AnimalDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return result != null ? ServiceResult.Success(result) : ServiceResult.Failed<AnimalDto>(ServiceError.NotFount);
        }
    }
}
