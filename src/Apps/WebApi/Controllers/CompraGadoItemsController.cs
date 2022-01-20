using System.Threading.Tasks;
using Application.Common.Models;
using Application.CompraGadoItems.Queries.GetCompraGadoItemsWithPagination;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class CompraGadoItemsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<CompraGadoDto>>>> GetAllCompraGadoItemsWithPagination(GetAllCompraGadoItemsWithPaginationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
