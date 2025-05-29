using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.RelationshipEntities;
using Domain.Entities.ResultEntities;

namespace Domain.Entities.CoreEntities
{
    public class FantasyTeam
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SeasonId { get; set; }
        public string TeamName { get; set; }
        public string? TeamLogoUrl { get; set; }
        public decimal Budget { get; set; }
        public int TransfersRemaining { get; set; }
        public int TotalPoints { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Season Season { get; set; }
        public ICollection<TeamDriver> TeamDrivers { get; set; }
        public ICollection<TeamConstructor> TeamConstructors { get; set; }
        public ICollection<GameweekTeam> GameweekTeams { get; set; }
    }

}
