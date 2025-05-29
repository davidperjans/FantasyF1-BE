using Application.Common;
using Application.Features.DriverFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using Domain.Entities.ResultEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.Queries.GetDriverStats
{
    public class GetDriverStatsQueryHandler : IRequestHandler<GetDriverStatsQuery, OperationResult<DriverStatsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDriverStatsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<DriverStatsDto>> Handle(GetDriverStatsQuery request, CancellationToken cancellationToken)
        {
            var driverRepo = _unitOfWork.Repository<Driver>();
            var resultRepo = _unitOfWork.Repository<RaceResult>();

            var driver = await driverRepo.GetByIdAsync(request.DriverId);
            if (driver is null)
                return OperationResult<DriverStatsDto>.Failure("Driver not found.");

            var results = await resultRepo.FindAllAsync(r => r.DriverId == request.DriverId, cancellationToken);

            var stats = new DriverStatsDto
            {
                DriverId = driver.Id,
                FullName = $"{driver.FirstName} {driver.LastName}",
                TotalPoints = results.Sum(r => r.FantasyPoints),
                RacesCompleted = results.Count(r => !r.DidNotStart && !r.Disqualified),
                Wins = results.Count(r => r.Position == 1),
                PolePositions = results.Count(r => r.PolePosition),
                FastestLaps = results.Count(r => r.FastestLap),
                DNFCount = results.Count(r => r.DidNotFinish)
            };

            return OperationResult<DriverStatsDto>.Success(stats);
        }
    }
}
