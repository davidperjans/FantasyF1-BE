using Application.Common;
using Application.Features.ConstructorFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ConstructorFeatures.Queries.GetConstructorStats
{
    public class GetConstructorStatsQuery : IRequest<OperationResult<ConstructorStatsDto>>
    {
        public Guid ConstructorId { get; set; }
    }
}
