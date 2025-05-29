using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Domain.Entities.ResultEntities
{
    public class ConstructorResult
    {
        public Guid Id { get; set; }
        public Guid RaceId { get; set; }
        public Guid ConstructorId { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public int FantasyPoints { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Race Race { get; set; }
        public Constructor Constructor { get; set; }
    }

}
