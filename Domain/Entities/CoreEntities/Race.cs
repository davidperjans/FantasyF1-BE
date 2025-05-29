using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ResultEntities;
using Domain.Enums;

namespace Domain.Entities.CoreEntities
{
    public class Race
    {
        public Guid Id { get; set; }
        public Guid SeasonId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Circuit { get; set; }
        public DateTime RaceDate { get; set; }
        public DateTime? QualifyingDate { get; set; }
        public DateTime? Practice1Date { get; set; }
        public DateTime? Practice2Date { get; set; }
        public DateTime? Practice3Date { get; set; }
        public int Round { get; set; }
        public RaceStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public Season Season { get; set; }
        public ICollection<RaceResult> RaceResults { get; set; }
        public ICollection<ConstructorResult> ConstructorResults { get; set; }
        public ICollection<GameweekTeam> GameweekTeams { get; set; }
    }

}
