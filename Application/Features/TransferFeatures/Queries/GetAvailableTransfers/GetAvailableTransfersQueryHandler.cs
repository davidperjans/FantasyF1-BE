using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.TransferFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using MediatR;

namespace Application.Features.TransferFeatures.Queries.GetAvailableTransfers
{
    public class GetAvailableTransfersQueryHandler : IRequestHandler<GetAvailableTransfersQuery, OperationResult<AvailableTransfersDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAvailableTransfersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<AvailableTransfersDto>> Handle(GetAvailableTransfersQuery request, CancellationToken cancellationToken)
        {
            var fantasyTeamRepo = _unitOfWork.Repository<FantasyTeam>();
            var driverRepo = _unitOfWork.Repository<Driver>();
            var constructorRepo = _unitOfWork.Repository<Constructor>();

            // Hämta teamet med navigation properties inkluderade
            var teams = await fantasyTeamRepo.FindAllWithIncludesAsync(
                t => t.Id == request.FantasyTeamId,
                new[] { "TeamDrivers", "TeamConstructors" },
                cancellationToken);

            var team = teams.FirstOrDefault();

            if (team == null)
            {
                Console.WriteLine("Team not found");
                return OperationResult<AvailableTransfersDto>.Failure("Fantasy team not found.");
            }

            Console.WriteLine($"TeamDrivers count: {team.TeamDrivers?.Count ?? 0}");
            Console.WriteLine($"TeamConstructors count: {team.TeamConstructors?.Count ?? 0}");

            var currentDriverIds = team.TeamDrivers?.Select(td => td.DriverId).ToHashSet() ?? new HashSet<Guid>();
            var currentConstructorIds = team.TeamConstructors?.Select(tc => tc.ConstructorId).ToHashSet() ?? new HashSet<Guid>();

            Console.WriteLine($"Current Driver IDs in team: {string.Join(", ", currentDriverIds)}");
            Console.WriteLine($"Current Constructor IDs in team: {string.Join(", ", currentConstructorIds)}");

            var allDrivers = await driverRepo.FindAllAsync(d => d.IsActive, cancellationToken);
            var allConstructors = await constructorRepo.FindAllAsync(c => c.IsActive, cancellationToken);

            var availableDrivers = allDrivers
                .Where(d => !currentDriverIds.Contains(d.Id))
                .ToList();

            var availableConstructors = allConstructors
                .Where(c => !currentConstructorIds.Contains(c.Id))
                .ToList();

            Console.WriteLine($"Available Drivers count: {availableDrivers.Count}");
            Console.WriteLine($"Available Constructors count: {availableConstructors.Count}");

            var result = new AvailableTransfersDto
            {
                Drivers = availableDrivers.Select(d => new AvailableDriverDto
                {
                    Id = d.Id,
                    FullName = $"{d.FirstName} {d.LastName}",
                    Code = d.Code,
                    Price = d.CurrentPrice,
                    IsActive = d.IsActive
                }).ToList(),

                Constructors = availableConstructors.Select(c => new AvailableConstructorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Price = c.CurrentPrice,
                    IsActive = c.IsActive
                }).ToList()
            };

            return OperationResult<AvailableTransfersDto>.Success(result);
        }
    }
}
