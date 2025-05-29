using Application.Common;
using Application.Features.ConstructorFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using Domain.Entities.ResultEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ConstructorFeatures.Queries.GetConstructorStats
{
    public class GetConstructorStatsQueryHandler : IRequestHandler<GetConstructorStatsQuery, OperationResult<ConstructorStatsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetConstructorStatsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<ConstructorStatsDto>> Handle(GetConstructorStatsQuery request, CancellationToken cancellationToken)
        {
            var constructorRepo = _unitOfWork.Repository<Constructor>();
            var resultRepo = _unitOfWork.Repository<ConstructorResult>();

            var constructor = await constructorRepo.GetByIdAsync(request.ConstructorId);
            if (constructor is null)
                return OperationResult<ConstructorStatsDto>.Failure("Constructor not found.");

            var results = await resultRepo.FindAllAsync(r => r.ConstructorId == request.ConstructorId, cancellationToken);

            var stats = new ConstructorStatsDto
            {
                ConstructorId = constructor.Id,
                Name = constructor.Name,
                TotalPoints = results.Sum(r => r.Points),
                FantasyPoints = results.Sum(r => r.FantasyPoints),
                Wins = results.Count(r => r.Position == 1),
                Races = results.Count
            };

            return OperationResult<ConstructorStatsDto>.Success(stats);
        }
    }
}
