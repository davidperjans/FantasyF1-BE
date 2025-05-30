using Application.Common;
using Application.Features.FantasyTeamFeatures.Commands.CreateFantasyTeam;
using Application.Features.FantasyTeamFeatures.Commands.UpdateFantasyTeam;
using Application.Features.FantasyTeamFeatures.DTOs;
using Application.Features.FantasyTeamFeatures.Queries.GetUserFantasyTeams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FantasyTeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FantasyTeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<OperationResult<FantasyTeamDto>>> Create([FromBody] CreateFantasyTeamCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("my-teams")]
        public async Task<ActionResult<OperationResult<List<FantasyTeamDto>>>> GetMyTeams([FromQuery] Guid userId)
        {
            var result = await _mediator.Send(new GetUserFantasyTeamsQuery { UserId = userId });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<OperationResult<FantasyTeamDto>>> Update(Guid id, [FromBody] UpdateFantasyTeamCommand command)
        {
            if (id != command.TeamId)
                return BadRequest(OperationResult<FantasyTeamDto>.Failure("Team ID in route and body do not match."));

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
