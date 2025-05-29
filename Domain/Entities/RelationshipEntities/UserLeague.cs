using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Domain.Entities.RelationshipEntities
{
    public class UserLeague
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LeagueId { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime? LeftAt { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public League League { get; set; }
    }

}
