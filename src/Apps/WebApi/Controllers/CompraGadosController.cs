using System.Threading.Tasks;
using Application.Common.Models;
using Application.CompraGados.Commands.Create;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class CompraGadosController: BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<CompraGadoDto>>> Create(CreateCompraGadoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
