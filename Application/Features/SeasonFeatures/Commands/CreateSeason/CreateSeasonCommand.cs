using Application.Common;
using Application.Features.SeasonFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SeasonFeatures.Commands.CreateSeason
{
    public class CreateSeasonCommand : IRequest<OperationResult<CreateSeasonResult>>
    {
        public int Year { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateSeasonResult
    {
        public SeasonDto Season { get; set; } = default!;
    }
}
