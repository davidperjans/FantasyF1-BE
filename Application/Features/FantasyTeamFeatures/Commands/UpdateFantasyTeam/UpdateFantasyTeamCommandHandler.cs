using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.FantasyTeamFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using Domain.Entities.RelationshipEntities;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Commands.UpdateFantasyTeam
{
    public class UpdateFantasyTeamCommandHandler : IRequestHandler<UpdateFantasyTeamCommand, OperationResult<FantasyTeamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private const decimal MAX_BUDGET = 100.0m;

        public UpdateFantasyTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<FantasyTeamDto>> Handle(UpdateFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var teamRepo = _unitOfWork.Repository<FantasyTeam>();
            var driverRepo = _unitOfWork.Repository<Driver>();
            var constructorRepo = _unitOfWork.Repository<Constructor>();

            var team = await teamRepo.GetByIdAsync(request.TeamId);
            if (team is null)
                return OperationResult<FantasyTeamDto>.Failure("Fantasy team not found.");

            var selectedDrivers = await driverRepo.FindAllAsync(d => request.Drivers.Select(x => x.DriverId).Contains(d.Id), cancellationToken);
            var selectedConstructors = await constructorRepo.FindAllAsync(c => request.Constructors.Select(x => x.ConstructorId).Contains(c.Id), cancellationToken);

            if (selectedDrivers.Count != 5 || selectedConstructors.Count != 2)
                return OperationResult<FantasyTeamDto>.Failure("Invalid number of drivers or constructors.");

            var totalCost = selectedDrivers.Sum(d => d.CurrentPrice) + selectedConstructors.Sum(c => c.CurrentPrice);
            if (totalCost > MAX_BUDGET)
                return OperationResult<FantasyTeamDto>.Failure($"Team exceeds budget ({totalCost:F1}M > {MAX_BUDGET:F1}M)");

            // Update name/logo
            team.TeamName = request.TeamName;
            team.TeamLogoUrl = request.TeamLogoUrl;
            team.Budget = MAX_BUDGET - totalCost;
            team.UpdatedAt = DateTime.UtcNow;

            // Clear existing drivers and constructors
            team.TeamDrivers.Clear();
            team.TeamConstructors.Clear();

            // Add new drivers
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

            // Add new constructors
            foreach (var input in request.Constructors)
            {
                var constructor = selectedConstructors.First(c => c.Id == input.ConstructorId);
                team.TeamConstructors.Add(new TeamConstructor
                {
                    Id = Guid.NewGuid(),
                    FantasyTeamId = team.Id,
                    ConstructorId = constructor.Id,
                    PurchasePrice = constructor.CurrentPrice,
                    AddedAt = DateTime.UtcNow
                });
            }

            await _unitOfWork.SaveChangesAsync();

            var dto = new FantasyTeamDto
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
                Constructor = team.TeamConstructors.Select(tc =>
                {
                    var c = selectedConstructors.First(x => x.Id == tc.ConstructorId);
                    return new TeamConstructorDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Price = tc.PurchasePrice
                    };
                }).First()
            };

            return OperationResult<FantasyTeamDto>.Success(dto);
        }
    }
}
