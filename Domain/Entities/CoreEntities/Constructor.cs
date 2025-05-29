using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.RelationshipEntities;
using Domain.Entities.ResultEntities;

namespace Domain.Entities.CoreEntities
{
    public class Constructor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string? LogoUrl { get; set; }
        public string? Color { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<SeasonConstructor> SeasonConstructors { get; set; }
        public ICollection<TeamConstructor> TeamConstructors { get; set; }
        public ICollection<ConstructorResult> ConstructorResults { get; set; }
    }

}
