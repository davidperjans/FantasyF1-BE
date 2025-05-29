using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.RelationshipEntities;

namespace Domain.Entities.CoreEntities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime? LastLoginAt { get; set; }

        // Navigation Properties
        public ICollection<FantasyTeam> FantasyTeams { get; set; }
        public ICollection<UserLeague> UserLeagues { get; set; }
        public ICollection<League> OwnedLeagues { get; set; }
    }

}
