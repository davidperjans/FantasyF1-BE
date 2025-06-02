using Application.Common;
using Application.Features.TransferFeatures.Commands.MakeTransfer;
using Application.Features.TransferFeatures.DTOs;
using Application.Features.TransferFeatures.Queries.GetAvailableTransfers;
using Application.Features.TransferFeatures.Queries.GetTransferHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available")]
        public async Task<ActionResult<OperationResult<AvailableTransfersDto>>> GetAvailableTransfers([FromQuery] GetAvailableTransfersQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("history/{fantasyTeamId}")]
        public async Task<ActionResult<OperationResult<List<TransferDto>>>> GetTransferHistory(Guid fantasyTeamId)
        {
            var result = await _mediator.Send(new GetTransferHistoryQuery { FantasyTeamId = fantasyTeamId });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("make")]
        public async Task<ActionResult<OperationResult<TransferDto>>> MakeTransfer([FromBody] MakeTransferCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
