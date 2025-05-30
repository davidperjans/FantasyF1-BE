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

            var teams = await fantasyTeamRepo.FindAllAsync(t => t.UserId == request.UserId, cancellationToken);

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

                Drivers = team.TeamDrivers.Select(td =>
                {
                    var driver = allDrivers.FirstOrDefault(d => d.Id == td.DriverId)!;
                    return new TeamDriverDto
                    {
                        Id = driver.Id,
                        FullName = $"{driver.FirstName} {driver.LastName}",
                        Code = driver.Code,
                        Price = td.PurchasePrice,
                        IsCaptain = td.IsCaptain
                    };
                }).ToList(),

                Constructor = team.TeamConstructors.Select(tc =>
                {
                    var constructor = allConstructors.FirstOrDefault(c => c.Id == tc.ConstructorId)!;
                    return new TeamConstructorDto
                    {
                        Id = constructor.Id,
                        Name = constructor.Name,
                        Price = tc.PurchasePrice
                    };
                }).First()
            }).ToList();

            return OperationResult<List<FantasyTeamDto>>.Success(teamDtos);
        }
    }
}
