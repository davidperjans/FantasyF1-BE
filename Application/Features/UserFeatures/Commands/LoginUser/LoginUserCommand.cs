using Application.Common;
using Application.Features.UserFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<OperationResult<LoginUserResult>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserResult
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = default!;
    }
}
