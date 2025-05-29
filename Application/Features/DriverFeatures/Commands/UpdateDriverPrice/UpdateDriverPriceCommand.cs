using Application.Common;
using Application.Features.DriverFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.Commands.UpdateDriverPrice
{
    public class UpdateDriverPriceCommand : IRequest<OperationResult<DriverDto>>
    {
        public Guid DriverId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
