using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(30);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Måste innehålla minst en versal")
                .Matches("[a-z]").WithMessage("Måste innehålla minst en gemen")
                .Matches("[0-9]").WithMessage("Måste innehålla minst en siffra")
                .Matches(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]").WithMessage("Måste innehålla ett specialtecken");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Lösenorden matchar inte");
        }
    }
}
