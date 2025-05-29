using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ConstructorFeatures.DTOs
{
    public class ConstructorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? Color { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
