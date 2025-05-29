using Application.Common;
using Application.Features.DriverFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.Queries.GetDriverStats
{
    public class GetDriverStatsQuery : IRequest<OperationResult<DriverStatsDto>>
    {
        public Guid DriverId { get; set; }
    }
}
