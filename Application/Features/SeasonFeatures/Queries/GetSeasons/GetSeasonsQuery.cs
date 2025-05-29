using Application.Common;
using Application.Features.SeasonFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SeasonFeatures.Queries.GetSeasons
{
    public class GetSeasonsQuery : IRequest<OperationResult<List<SeasonDto>>>
    {
        public int? Year { get; set; }
        public bool? IsActive { get; set; }
    }
}
