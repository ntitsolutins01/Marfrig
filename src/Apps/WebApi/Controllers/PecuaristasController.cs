using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Dto;
using Application.Pecuaristas.Commands.Create;
using Application.Pecuaristas.Commands.Delete;
using Application.Pecuaristas.Commands.Update;
using Application.Pecuaristas.Queries.GetPecuaristaById;
using Application.Pecuaristas.Queries.GetPecuaristas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class PecuaristasController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<PecuaristaDto>>>> GetAllPecuaristas(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllPecuaristasQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<PecuaristaDto>>> GetCityById(int id)
        {
            return Ok(await Mediator.Send(new GetPecuaristaByIdQuery { PecuaristaId = id }));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<PecuaristaDto>>> Create(CreatePecuaristaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<PecuaristaDto>>> Update(UpdatePecuaristaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<PecuaristaDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePecuaristaCommand { Id = id }));
        }
    }
}
