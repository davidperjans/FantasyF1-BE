using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.FantasyTeamFeatures.DTOs;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Queries.GetUserFantasyTeams
{
    public class GetUserFantasyTeamsQuery : IRequest<OperationResult<List<FantasyTeamDto>>>
    {
        public Guid UserId { get; set; }
    }
}
