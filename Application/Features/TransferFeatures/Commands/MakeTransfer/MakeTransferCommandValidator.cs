using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.TransferFeatures.Commands.MakeTransfer
{
    public class MakeTransferCommandValidator : AbstractValidator<MakeTransferCommand>
    {
        public MakeTransferCommandValidator()
        {
            RuleFor(x => x.FantasyTeamId).NotEmpty();

            RuleFor(x => x)
                .Must(x =>
                {
                    var driverChange = x.OutDriverId != Guid.Empty || x.InDriverId != Guid.Empty;
                    var constructorChange = x.OutConstructorId != Guid.Empty || x.InConstructorId != Guid.Empty;
                    return driverChange || constructorChange;
                }).WithMessage("You must transfer at least one driver or constructor.");

            RuleFor(x => x)
                .Must(x =>
                {
                    var driverChangeValid = (x.OutDriverId == Guid.Empty) == (x.InDriverId == Guid.Empty);
                    var constructorChangeValid = (x.OutConstructorId == Guid.Empty) == (x.InConstructorId == Guid.Empty);
                    return driverChangeValid && constructorChangeValid;
                }).WithMessage("Out and In IDs for driver and constructor must be paired.");
        }
    }
}
