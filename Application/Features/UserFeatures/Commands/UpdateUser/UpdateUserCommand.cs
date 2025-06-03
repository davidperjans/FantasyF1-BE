using Application.Common;
using Application.Features.UserFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<OperationResult<bool>>
    {
        public Guid UserId { get; set; }
        public UpdateUserDto Dto { get; set; } = null!;
    }
}
