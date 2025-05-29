using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Domain.Entities.ResultEntities
{
    public class GameweekConstructor
    {
        public Guid Id { get; set; }
        public Guid GameweekTeamId { get; set; }
        public Guid ConstructorId { get; set; }
        public int Points { get; set; }
        public decimal Price { get; set; }

        // Navigation Properties
        public GameweekTeam GameweekTeam { get; set; }
        public Constructor Constructor { get; set; }
    }
}
