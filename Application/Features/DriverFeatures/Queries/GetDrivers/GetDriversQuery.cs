using Application.Common;
using Application.Features.DriverFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.Queries.GetDrivers
{
    public class GetDriversQuery : IRequest<OperationResult<List<DriverDto>>>
    {
        public string? Search { get; set; } // valfritt sökord (namn/kod)
        public bool? IsActive { get; set; } // valfri filtrering
    }
}
