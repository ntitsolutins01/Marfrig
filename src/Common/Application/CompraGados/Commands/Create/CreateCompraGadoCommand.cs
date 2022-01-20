using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Dto;
using Domain.Entities;
using MapsterMapper;

namespace Application.CompraGados.Commands.Create
{
    public class CreateCompraGadoCommand : IRequestWrapper<CompraGadoDto>
    {
        public string Nome { get; set; }
        public int IdPecuarista { get; set; }
        public DateTime DataEntrega { get; set; }
    }

    public class CreateCompraGadoCommandHandler : IRequestHandlerWrapper<CreateCompraGadoCommand, CompraGadoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCompraGadoCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CompraGadoDto>> Handle(CreateCompraGadoCommand request, CancellationToken cancellationToken)
        {
            var entity = new CompraGado
            {
                Nome = request.Nome,
                IdPecuarista = request.IdPecuarista,
                DataEntrega = request.DataEntrega
            };

            await _context.CompraGados.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CompraGadoDto>(entity));
        }
    }
}
