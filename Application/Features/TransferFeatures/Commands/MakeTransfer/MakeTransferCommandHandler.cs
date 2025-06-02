using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.TransferFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using Domain.Entities.RelationshipEntities;
using MediatR;

namespace Application.Features.TransferFeatures.Commands.MakeTransfer
{
    public class MakeTransferCommandHandler : IRequestHandler<MakeTransferCommand, OperationResult<TransferDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private const decimal MAX_BUDGET = 100.0m;

        public MakeTransferCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<TransferDto>> Handle(MakeTransferCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fantasyTeamRepo = (IFantasyTeamRepository)_unitOfWork.Repository<FantasyTeam>();
                var driverRepo = _unitOfWork.Repository<Driver>();
                var constructorRepo = _unitOfWork.Repository<Constructor>();
                var transferRepo = _unitOfWork.Repository<Transfer>();
                var teamDriverRepo = _unitOfWork.Repository<TeamDriver>();
                var teamConstructorRepo = _unitOfWork.Repository<TeamConstructor>();

                var team = await fantasyTeamRepo.GetTeamWithDetailsAsync(request.FantasyTeamId, cancellationToken);
                if (team == null)
                    return OperationResult<TransferDto>.Failure("Fantasy team not found.");

                // Validera att OutDriver finns i laget
                if (request.OutDriverId != Guid.Empty && !team.TeamDrivers.Any(td => td.DriverId == request.OutDriverId))
                    return OperationResult<TransferDto>.Failure("Driver to be transferred out not found in team.");

                // Validera att OutConstructor finns i laget
                if (request.OutConstructorId != Guid.Empty && !team.TeamConstructors.Any(tc => tc.ConstructorId == request.OutConstructorId))
                    return OperationResult<TransferDto>.Failure("Constructor to be transferred out not found in team.");

                var inDriver = request.InDriverId != Guid.Empty
                    ? await driverRepo.GetByIdAsync(request.InDriverId)
                    : null;

                var inConstructor = request.InConstructorId != Guid.Empty
                    ? await constructorRepo.GetByIdAsync(request.InConstructorId)
                    : null;

                if (inDriver == null && request.InDriverId != Guid.Empty)
                    return OperationResult<TransferDto>.Failure("Driver to be transferred in not found.");

                if (inConstructor == null && request.InConstructorId != Guid.Empty)
                    return OperationResult<TransferDto>.Failure("Constructor to be transferred in not found.");

                // Beräkna budget
                decimal currentTotalPrice = team.TeamDrivers.Sum(td => td.Driver.CurrentPrice) +
                                           team.TeamConstructors.Sum(tc => tc.Constructor.CurrentPrice);

                decimal outPrice = 0;
                decimal inPrice = 0;

                if (request.OutDriverId != Guid.Empty)
                {
                    var outDriver = team.TeamDrivers.First(td => td.DriverId == request.OutDriverId).Driver;
                    outPrice += outDriver.CurrentPrice;
                }
                if (request.OutConstructorId != Guid.Empty)
                {
                    var outConstructor = team.TeamConstructors.First(tc => tc.ConstructorId == request.OutConstructorId).Constructor;
                    outPrice += outConstructor.CurrentPrice;
                }
                if (inDriver != null)
                    inPrice += inDriver.CurrentPrice;
                if (inConstructor != null)
                    inPrice += inConstructor.CurrentPrice;

                decimal newTotalPrice = currentTotalPrice - outPrice + inPrice;

                if (newTotalPrice > MAX_BUDGET)
                    return OperationResult<TransferDto>.Failure($"Transfer exceeds budget: new total price {newTotalPrice:F1}M is over {MAX_BUDGET}M.");

                // HUVUDFÖRÄNDRING: Använd separata operationer istället för att modifiera collection

                // Ta bort gamla spelare genom att sätta RemovedAt (om du har detta fält)
                // Eller använd repository.Delete() istället för collection.Remove()
                if (request.OutDriverId != Guid.Empty)
                {
                    var teamDriverToRemove = team.TeamDrivers.First(td => td.DriverId == request.OutDriverId);
                    // Istället för team.TeamDrivers.Remove(teamDriverToRemove);
                    teamDriverRepo.Remove(teamDriverToRemove);
                }

                if (request.OutConstructorId != Guid.Empty)
                {
                    var teamConstructorToRemove = team.TeamConstructors.First(tc => tc.ConstructorId == request.OutConstructorId);
                    // Istället för team.TeamConstructors.Remove(teamConstructorToRemove);
                    teamConstructorRepo.Remove(teamConstructorToRemove);
                }

                // Lägg till nya spelare
                if (inDriver != null)
                {
                    var newTeamDriver = new TeamDriver
                    {
                        Id = Guid.NewGuid(),
                        FantasyTeamId = team.Id,
                        DriverId = inDriver.Id,
                        IsCaptain = false,
                        PurchasePrice = inDriver.CurrentPrice,
                        AddedAt = DateTime.UtcNow
                    };
                    await teamDriverRepo.AddAsync(newTeamDriver);
                }

                if (inConstructor != null)
                {
                    var newTeamConstructor = new TeamConstructor
                    {
                        Id = Guid.NewGuid(),
                        FantasyTeamId = team.Id,
                        ConstructorId = inConstructor.Id,
                        PurchasePrice = inConstructor.CurrentPrice,
                        AddedAt = DateTime.UtcNow
                    };
                    await teamConstructorRepo.AddAsync(newTeamConstructor);
                }

                // Spara transfer historik
                var transfer = new Transfer
                {
                    Id = Guid.NewGuid(),
                    FantasyTeamId = team.Id,
                    OutDriverId = request.OutDriverId != Guid.Empty ? request.OutDriverId : null,
                    InDriverId = request.InDriverId != Guid.Empty ? request.InDriverId : null,
                    OutConstructorId = request.OutConstructorId != Guid.Empty ? request.OutConstructorId : null,
                    InConstructorId = request.InConstructorId != Guid.Empty ? request.InConstructorId : null,
                    TransferDate = DateTime.UtcNow
                };
                await transferRepo.AddAsync(transfer);

                // Spara alla ändringar
                await _unitOfWork.SaveChangesAsync();

                var transferDto = new TransferDto
                {
                    Id = transfer.Id,
                    FantasyTeamId = transfer.FantasyTeamId,
                    OutDriverId = transfer.OutDriverId,
                    InDriverId = transfer.InDriverId,
                    OutConstructorId = transfer.OutConstructorId,
                    InConstructorId = transfer.InConstructorId,
                    TransferDate = transfer.TransferDate
                };

                return OperationResult<TransferDto>.Success(transferDto);
            }
            catch (Exception ex)
            {
                return OperationResult<TransferDto>.Failure($"Transfer failed: {ex.Message}");
            }
        }
    }
}
