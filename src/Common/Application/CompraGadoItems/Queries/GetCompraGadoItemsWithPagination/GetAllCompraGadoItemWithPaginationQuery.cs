using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Application.Dto;
using Mapster;
using MapsterMapper;

namespace Application.CompraGadoItems.Queries.GetCompraGadoItemsWithPagination
{
    public class GetAllCompraGadoItemsWithPaginationQuery : IRequestWrapper<PaginatedList<CompraGadoItemDto>>
    {
        public int IdCompraGadoItem { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllCompraGadoItemsWithPaginationQueryHandler : IRequestHandlerWrapper<GetAllCompraGadoItemsWithPaginationQuery, PaginatedList<CompraGadoItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCompraGadoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<CompraGadoItemDto>>> Handle(GetAllCompraGadoItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<CompraGadoItemDto> list = await _context.CompraGadoItems
                .Where(x => x.IdCompraGado == request.IdCompraGadoItem)
                .OrderBy(o => o.Id)
                .ProjectToType<CompraGadoItemDto>(_mapper.Config)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<CompraGadoItemDto>>(ServiceError.NotFount);
        }
    }
}
