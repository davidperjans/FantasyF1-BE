using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ConstructorFeatures.DTOs
{
    public class ConstructorStatsDto
    {
        public Guid ConstructorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalPoints { get; set; }
        public int FantasyPoints { get; set; }
        public int Wins { get; set; }
        public int Races { get; set; }
    }
}
