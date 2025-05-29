using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Domain.Entities.RelationshipEntities
{
    public class TeamDriver
    {
        public Guid Id { get; set; }
        public Guid FantasyTeamId { get; set; }
        public Guid DriverId { get; set; }
        public bool IsCaptain { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? RemovedAt { get; set; }

        // Navigation Properties
        public FantasyTeam FantasyTeam { get; set; }
        public Driver Driver { get; set; }
    }

}
