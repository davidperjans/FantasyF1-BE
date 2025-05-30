using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransferFeatures.DTOs
{
    public class TransferDto
    {
        public Guid Id { get; set; }
        public Guid FantasyTeamId { get; set; }
        public Guid? OutDriverId { get; set; }
        public Guid? InDriverId { get; set; }
        public Guid? OutConstructorId { get; set; }
        public Guid? InConstructorId { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
