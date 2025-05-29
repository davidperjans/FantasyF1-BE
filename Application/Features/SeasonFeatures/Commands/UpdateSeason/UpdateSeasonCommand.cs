using Application.Common;
using Application.Features.SeasonFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SeasonFeatures.Commands.UpdateSeason
{
    public class UpdateSeasonCommand : IRequest<OperationResult<SeasonDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
    }
}
