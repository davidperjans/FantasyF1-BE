using Application.Common;
using Application.Features.SeasonFeatures.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.CoreEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SeasonFeatures.Queries.GetCurrentSeason
{
    public class GetCurrentSeasonQueryHandler : IRequestHandler<GetCurrentSeasonQuery, OperationResult<SeasonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCurrentSeasonQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<SeasonDto>> Handle(GetCurrentSeasonQuery request, CancellationToken cancellationToken)
        {
            var seasonRepo = _unitOfWork.Repository<Season>();

            Expression<Func<Season, bool>> filter = s => s.IsActive;
            var seasons = await seasonRepo.FindAllAsync(filter, cancellationToken);

            var current = seasons
                .OrderByDescending(s => s.StartDate)
                .FirstOrDefault();

            if (current is null)
                return OperationResult<SeasonDto>.Failure("No active season found.");

            var dto = _mapper.Map<SeasonDto>(current);
            return OperationResult<SeasonDto>.Success(dto);
        }
    }
}
