using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Dto.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Dto.Email));
            RuleFor(x => x.Dto.UserName).MinimumLength(3).When(x => !string.IsNullOrWhiteSpace(x.Dto.UserName));
        }
    }
}
