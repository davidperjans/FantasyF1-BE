using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SeasonFeatures.DTOs
{
    public class SeasonDto
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
    }
}
