using Application.Common;
using Application.Features.SeasonFeatures.Commands.CreateSeason;
using Application.Features.SeasonFeatures.Commands.UpdateSeason;
using Application.Features.SeasonFeatures.DTOs;
using Application.Features.SeasonFeatures.Queries.GetCurrentSeason;
using Application.Features.SeasonFeatures.Queries.GetSeasons;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeasonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeasonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<OperationResult<CreateSeasonResult>>> Create(CreateSeasonCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<OperationResult<List<SeasonDto>>>> GetAll([FromQuery] GetSeasonsQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("current")]
        public async Task<ActionResult<OperationResult<SeasonDto>>> GetCurrent()
        {
            var result = await _mediator.Send(new GetCurrentSeasonQuery());
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<OperationResult<SeasonDto>>> Update(Guid id, UpdateSeasonCommand command)
        {
            if (id != command.Id)
                return BadRequest(OperationResult<SeasonDto>.Failure("ID mismatch."));

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
