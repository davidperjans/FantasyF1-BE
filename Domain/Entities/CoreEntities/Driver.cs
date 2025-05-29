using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.RelationshipEntities;
using Domain.Entities.ResultEntities;

namespace Domain.Entities.CoreEntities
{
    public class Driver
    {
        public Guid Id { get; set; }
        public int DriverNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; } // HAM, VER, etc.
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<SeasonDriver> SeasonDrivers { get; set; }
        public ICollection<TeamDriver> TeamDrivers { get; set; }
        public ICollection<RaceResult> RaceResults { get; set; }
    }
}
