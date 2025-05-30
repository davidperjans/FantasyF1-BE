using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ConstructorFeatures.DTOs;
using Application.Features.DriverFeatures.DTOs;

namespace Application.Features.FantasyTeamFeatures.DTOs
{
    public class FantasyTeamDto
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string? TeamLogoUrl { get; set; }
        public decimal Budget { get; set; }
        public int TotalPoints { get; set; }
        public int TransfersRemaining { get; set; }

        public List<TeamDriverDto> Drivers { get; set; } = new();
        public List<TeamConstructorDto> Constructors { get; set; } = default!;
    }
}
