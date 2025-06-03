using Application.Common;
using Application.Features.UserFeatures.Commands.UpdateUser;
using Application.Features.UserFeatures.DTOs;
using Application.Features.UserFeatures.Queries.GetCurrentUser;
using Application.Features.UserFeatures.Queries.SearchUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        public async Task<ActionResult<OperationResult<UserDto>>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userId, out var currentUserId))
                return Unauthorized();

            var query = new GetCurrentUserQuery(currentUserId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPatch("profile")]
        public async Task<ActionResult<OperationResult<bool>>> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userId, out var currentUserId))
                return Unauthorized();

            var command = new UpdateUserCommand { UserId = currentUserId, Dto = dto };

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<OperationResult<List<UserDto>>>> SearchUsers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(OperationResult<List<UserDto>>.Failure("Search query cannot be empty."));

            var result = await _mediator.Send(new SearchUsersQuery { Query = query });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
