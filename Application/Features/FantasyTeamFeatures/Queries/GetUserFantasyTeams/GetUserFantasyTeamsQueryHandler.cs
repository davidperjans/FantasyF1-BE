using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.FantasyTeamFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Queries.GetUserFantasyTeams
{
    public class GetUserFantasyTeamsQueryHandler : IRequestHandler<GetUserFantasyTeamsQuery, OperationResult<List<FantasyTeamDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserFantasyTeamsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<List<FantasyTeamDto>>> Handle(GetUserFantasyTeamsQuery request, CancellationToken cancellationToken)
        {
            var fantasyTeamRepo = _unitOfWork.Repository<FantasyTeam>();
            var driverRepo = _unitOfWork.Repository<Driver>();
            var constructorRepo = _unitOfWork.Repository<Constructor>();

            var includes = new[] { "TeamDrivers", "TeamConstructors" };

            var teams = await fantasyTeamRepo.FindAllWithIncludesAsync(
                t => t.UserId == request.UserId,
                includes,
                cancellationToken
            );

            var allDrivers = await driverRepo.FindAllAsync(cancellationToken: cancellationToken);
            var allConstructors = await constructorRepo.FindAllAsync(cancellationToken: cancellationToken);

            var teamDtos = teams.Select(team => new FantasyTeamDto
            {
                Id = team.Id,
                TeamName = team.TeamName,
                TeamLogoUrl = team.TeamLogoUrl,
                Budget = team.Budget,
                TotalPoints = team.TotalPoints,
                TransfersRemaining = team.TransfersRemaining,

                Drivers = team.TeamDrivers?.Any() == true
                    ? team.TeamDrivers.Select(td =>
                    {
                        var driver = allDrivers.FirstOrDefault(d => d.Id == td.DriverId);
                        return new TeamDriverDto
                        {
                            Id = driver?.Id ?? Guid.Empty,
                            FullName = driver != null ? $"{driver.FirstName} {driver.LastName}" : "Unknown Driver",
                            Code = driver?.Code ?? "N/A",
                            Price = td.PurchasePrice,
                            IsCaptain = td.IsCaptain
                        };
                    }).ToList()
                    : new List<TeamDriverDto>(),

                Constructors = team.TeamConstructors?.Any() == true
                    ? team.TeamConstructors.Select(tc =>
                    {
                        var constructor = allConstructors.FirstOrDefault(c => c.Id == tc.ConstructorId);
                        return new TeamConstructorDto
                        {
                            Id = constructor?.Id ?? Guid.Empty,
                            Name = constructor?.Name ?? "Unknown",
                            Price = tc.PurchasePrice
                        };
                    }).ToList()
                    : new List<TeamConstructorDto>()
                }).ToList();

            return OperationResult<List<FantasyTeamDto>>.Success(teamDtos);
        }
    }
}
