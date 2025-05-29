using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.RelationshipEntities;

namespace Domain.Entities.CoreEntities
{
    public class League
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid SeasonId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string InviteCode { get; set; }
        public bool IsPublic { get; set; }
        public int MaxMembers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public User Owner { get; set; }
        public Season Season { get; set; }
        public ICollection<UserLeague> UserLeagues { get; set; }
    }
}
