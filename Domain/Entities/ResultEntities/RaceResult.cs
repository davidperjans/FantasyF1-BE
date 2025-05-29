using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Domain.Entities.ResultEntities
{
    public class RaceResult
    {
        public Guid Id { get; set; }
        public Guid RaceId { get; set; }
        public Guid DriverId { get; set; }
        public int Position { get; set; }
        public int? GridPosition { get; set; }
        public int Points { get; set; }
        public TimeSpan? Time { get; set; }
        public bool FastestLap { get; set; }
        public bool PolePosition { get; set; }
        public bool DidNotFinish { get; set; }
        public bool DidNotStart { get; set; }
        public bool Disqualified { get; set; }
        public int FantasyPoints { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Race Race { get; set; }
        public Driver Driver { get; set; }
    }
}
