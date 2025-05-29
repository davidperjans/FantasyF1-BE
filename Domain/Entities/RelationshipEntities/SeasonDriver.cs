using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Domain.Entities.RelationshipEntities
{
    public class SeasonDriver
    {
        public Guid Id { get; set; }
        public Guid SeasonId { get; set; }
        public Guid DriverId { get; set; }
        public Guid? ConstructorId { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public int TotalPoints { get; set; }

        // Navigation Properties
        public Season Season { get; set; }
        public Driver Driver { get; set; }
        public Constructor? Constructor { get; set; }
    }
}
