using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.FantasyTeamFeatures.DTOs;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Commands.CreateFantasyTeam
{
    public class CreateFantasyTeamCommand : IRequest<OperationResult<FantasyTeamDto>>
    {
        public Guid UserId { get; set; }
        public Guid SeasonId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string? TeamLogoUrl { get; set; }

        public List<CreateDriverInput> Drivers { get; set; } = new();
        public List<CreateConstructorInput> Constructors { get; set; } = new();
    }

    public class CreateDriverInput
    {
        public Guid DriverId { get; set; }
        public bool IsCaptain { get; set; }
    }

    public class CreateConstructorInput
    {
        public Guid ConstructorId { get; set; }
    }
}
