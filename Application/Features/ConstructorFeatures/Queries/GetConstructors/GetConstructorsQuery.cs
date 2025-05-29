using Application.Common;
using Application.Features.ConstructorFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ConstructorFeatures.Queries.GetConstructors
{
    public class GetConstructorsQuery : IRequest<OperationResult<List<ConstructorDto>>>
    {
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
    }
}
