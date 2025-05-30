using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FantasyTeamFeatures.DTOs
{
    public class TeamDriverDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsCaptain { get; set; }
    }
}
