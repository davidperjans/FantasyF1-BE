using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.DTOs
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        public int DriverNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
