using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.TransferFeatures.DTOs;
using MediatR;

namespace Application.Features.TransferFeatures.Queries.GetAvailableTransfers
{
    public class GetAvailableTransfersQuery : IRequest<OperationResult<AvailableTransfersDto>>
    {
        public Guid FantasyTeamId { get; set; }
    }
}
