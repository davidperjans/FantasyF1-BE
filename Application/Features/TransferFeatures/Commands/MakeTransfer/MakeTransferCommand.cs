using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.TransferFeatures.DTOs;
using MediatR;

namespace Application.Features.TransferFeatures.Commands.MakeTransfer
{
    public class MakeTransferCommand : IRequest<OperationResult<TransferDto>>
    {
        public Guid FantasyTeamId { get; set; }
        public Guid OutDriverId { get; set; } // kan vara Guid.Empty om ingen
        public Guid InDriverId { get; set; }  // kan vara Guid.Empty om ingen

        public Guid OutConstructorId { get; set; } // kan vara Guid.Empty om ingen
        public Guid InConstructorId { get; set; }  // kan vara Guid.Empty om ingen
    }
}
