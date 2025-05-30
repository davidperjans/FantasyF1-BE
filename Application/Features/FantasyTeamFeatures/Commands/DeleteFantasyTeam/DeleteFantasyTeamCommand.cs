using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Commands.DeleteFantasyTeam
{
    public class DeleteFantasyTeamCommand : IRequest<OperationResult<bool>>
    {
        public Guid FantasyTeamId { get; set; }
        public Guid UserId { get; set; }

        public DeleteFantasyTeamCommand(Guid fantasyTeamId, Guid userId)
        {
            FantasyTeamId = fantasyTeamId;
            UserId = userId;
        }
    }
}
