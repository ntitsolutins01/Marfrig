using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Dto;
using Application.Animais.Commands.Create;
using Application.Animais.Commands.Delete;
using Application.Animais.Commands.Update;
using Application.Animais.Queries.GetAnimalById;
using Application.Animais.Queries.GetAnimais;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class AnimaisController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<AnimalDto>>>> GetAllAnimais(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllAnimaisQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<AnimalDto>>> GetCityById(int id)
        {
            return Ok(await Mediator.Send(new GetAnimalByIdQuery { AnimalId = id }));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<AnimalDto>>> Create(CreateAnimalCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<AnimalDto>>> Update(UpdateAnimalCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<AnimalDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAnimalCommand { Id = id }));
        }
    }
}
