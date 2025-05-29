using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.RelationshipEntities;

namespace Domain.Entities.CoreEntities
{
    public class Season
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ICollection<Race> Races { get; set; }
        public ICollection<SeasonDriver> SeasonDrivers { get; set; }
        public ICollection<SeasonConstructor> SeasonConstructors { get; set; }
        public ICollection<FantasyTeam> FantasyTeams { get; set; }
    }

}
