using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.FantasyTeamFeatures.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.CoreEntities;
using Domain.Entities.RelationshipEntities;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Commands.CreateFantasyTeam
{
    public class CreateFantasyTeamCommandHandler : IRequestHandler<CreateFantasyTeamCommand, OperationResult<FantasyTeamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private const decimal MAX_BUDGET = 100.0m;

        public CreateFantasyTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<FantasyTeamDto>> Handle(CreateFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var driverRepo = _unitOfWork.Repository<Driver>();
            var constructorRepo = _unitOfWork.Repository<Constructor>();
            var fantasyTeamRepo = _unitOfWork.Repository<FantasyTeam>();

            var existingTeams = await fantasyTeamRepo.FindAllAsync(t => t.UserId == request.UserId && t.SeasonId == request.SeasonId, cancellationToken);

            if (existingTeams.Count >= 3)
                return OperationResult<FantasyTeamDto>.Failure("You can only create up to 3 fantasy teams per season.");

            var selectedDrivers = await driverRepo.FindAllAsync(d => request.Drivers.Select(x => x.DriverId).Contains(d.Id), cancellationToken);
            var selectedConstructors = await constructorRepo.FindAllAsync(c => request.Constructors.Select(x => x.ConstructorId).Contains(c.Id), cancellationToken);

            if (selectedDrivers.Count != 2)
                return OperationResult<FantasyTeamDto>.Failure("You must select exactly 2 valid drivers.");

            if (selectedConstructors.Count != 1)
                return OperationResult<FantasyTeamDto>.Failure("You must select exactly 1 valid constructor.");

            var totalCost = selectedDrivers.Sum(d => d.CurrentPrice) + selectedConstructors.Sum(c => c.CurrentPrice);
            if (totalCost > MAX_BUDGET)
                return OperationResult<FantasyTeamDto>.Failure($"Selected drivers and constructor exceed budget ({totalCost:F1}M > {MAX_BUDGET:F1}M)");

            // Create FantasyTeam
            var team = new FantasyTeam
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                SeasonId = request.SeasonId,
                TeamName = request.TeamName,
                TeamLogoUrl = request.TeamLogoUrl,
                Budget = MAX_BUDGET - totalCost,
                TransfersRemaining = 5,
                TotalPoints = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TeamDrivers = new List<TeamDriver>(),
                TeamConstructors = new List<TeamConstructor>()
            };

            // Add Drivers
            foreach (var input in request.Drivers)
            {
                var driver = selectedDrivers.First(d => d.Id == input.DriverId);

                team.TeamDrivers.Add(new TeamDriver
                {
                    Id = Guid.NewGuid(),
                    FantasyTeamId = team.Id,
                    DriverId = driver.Id,
                    IsCaptain = input.IsCaptain,
                    PurchasePrice = driver.CurrentPrice,
                    AddedAt = DateTime.UtcNow
                });
            }

            // Add Constructor
            var constructor = selectedConstructors.First();
            team.TeamConstructors.Add(new TeamConstructor
            {
                Id = Guid.NewGuid(),
                FantasyTeamId = team.Id,
                ConstructorId = constructor.Id,
                PurchasePrice = constructor.CurrentPrice,
                AddedAt = DateTime.UtcNow
            });

            await fantasyTeamRepo.AddAsync(team);
            await _unitOfWork.SaveChangesAsync();

            // Map to DTO
            var resultDto = new FantasyTeamDto
            {
                Id = team.Id,
                TeamName = team.TeamName,
                TeamLogoUrl = team.TeamLogoUrl,
                Budget = team.Budget,
                TotalPoints = team.TotalPoints,
                TransfersRemaining = team.TransfersRemaining,
                Drivers = team.TeamDrivers.Select(td =>
                {
                    var d = selectedDrivers.First(x => x.Id == td.DriverId);
                    return new TeamDriverDto
                    {
                        Id = d.Id,
                        FullName = $"{d.FirstName} {d.LastName}",
                        Code = d.Code,
                        Price = td.PurchasePrice,
                        IsCaptain = td.IsCaptain
                    };
                }).ToList(),
                Constructor = new TeamConstructorDto
                {
                    Id = constructor.Id,
                    Name = constructor.Name,
                    Price = constructor.CurrentPrice
                }
            };

            return OperationResult<FantasyTeamDto>.Success(resultDto);
        }
    }
}
