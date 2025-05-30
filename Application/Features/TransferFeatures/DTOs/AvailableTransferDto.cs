using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransferFeatures.DTOs
{
    public class AvailableTransfersDto
    {
        public List<AvailableDriverDto> Drivers { get; set; } = new();
        public List<AvailableConstructorDto> Constructors { get; set; } = new();
    }
}
