using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.FantasyTeamFeatures.DTOs;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Commands.UpdateFantasyTeam
{
    public class UpdateFantasyTeamCommand : IRequest<OperationResult<FantasyTeamDto>>
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string? TeamLogoUrl { get; set; }

        public List<UpdateDriverInput> Drivers { get; set; } = new();
        public List<UpdateConstructorInput> Constructors { get; set; } = new();
    }

    public class UpdateDriverInput
    {
        public Guid DriverId { get; set; }
        public bool IsCaptain { get; set; }
    }

    public class UpdateConstructorInput
    {
        public Guid ConstructorId { get; set; }
    }
}
