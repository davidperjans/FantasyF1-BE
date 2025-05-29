using Application.Common;
using Application.Features.ConstructorFeatures.DTOs;
using Application.Features.ConstructorFeatures.Queries.GetConstructors;
using Application.Features.ConstructorFeatures.Queries.GetConstructorStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConstructorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConstructorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<OperationResult<List<ConstructorDto>>>> GetAll([FromQuery] GetConstructorsQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<OperationResult<ConstructorStatsDto>>> GetStats(Guid id)
        {
            var result = await _mediator.Send(new GetConstructorStatsQuery { ConstructorId = id });
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
