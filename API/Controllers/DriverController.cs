using Application.Common;
using Application.Features.DriverFeatures.Commands.UpdateDriverPrice;
using Application.Features.DriverFeatures.DTOs;
using Application.Features.DriverFeatures.Queries.GetDrivers;
using Application.Features.DriverFeatures.Queries.GetDriverStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DriverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<OperationResult<List<DriverDto>>>> GetAll([FromQuery] GetDriversQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<OperationResult<DriverStatsDto>>> GetStats(Guid id)
        {
            var result = await _mediator.Send(new GetDriverStatsQuery { DriverId = id });
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPatch("{id}/update-price")]
        public async Task<ActionResult<OperationResult<DriverDto>>> UpdatePrice(Guid id, [FromBody] UpdateDriverPriceCommand command)
        {
            if (id != command.DriverId)
                return BadRequest(OperationResult<DriverDto>.Failure("ID mismatch."));

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
