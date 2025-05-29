using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Domain.Entities.ResultEntities
{
    public class GameweekTeam
    {
        public Guid Id { get; set; }
        public Guid FantasyTeamId { get; set; }
        public Guid RaceId { get; set; }
        public int Points { get; set; }
        public int TransfersUsed { get; set; }
        public int TransferCost { get; set; }
        public DateTime TeamLockTime { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public FantasyTeam FantasyTeam { get; set; }
        public Race Race { get; set; }
        public ICollection<GameweekDriver> GameweekDrivers { get; set; }
        public ICollection<GameweekConstructor> GameweekConstructors { get; set; }
    }
}
