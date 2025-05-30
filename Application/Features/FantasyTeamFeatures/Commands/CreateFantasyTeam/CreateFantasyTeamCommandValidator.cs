using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.FantasyTeamFeatures.Commands.CreateFantasyTeam
{
    public class CreateFantasyTeamCommandValidator : AbstractValidator<CreateFantasyTeamCommand>
    {
        public CreateFantasyTeamCommandValidator()
        {
            RuleFor(x => x.TeamName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SeasonId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Drivers)
                .Must(d => d.Count == 5)
                .WithMessage("You must select exactly 5 drivers.");

            RuleFor(x => x.Constructors)
                .Must(c => c.Count == 2)
                .WithMessage("You must select exactly 2 constructor.");

            RuleFor(x => x.Drivers)
                .Must(d => d.Count(d => d.IsCaptain) == 1)
                .WithMessage("Exactly one driver must be set as captain.");
        }
    }
}
