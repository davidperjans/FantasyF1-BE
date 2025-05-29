using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.DTOs
{
    public class DriverStatsDto
    {
        public Guid DriverId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int TotalPoints { get; set; }
        public int RacesCompleted { get; set; }
        public int Wins { get; set; }
        public int PolePositions { get; set; }
        public int FastestLaps { get; set; }
        public int DNFCount { get; set; }
    }
}
