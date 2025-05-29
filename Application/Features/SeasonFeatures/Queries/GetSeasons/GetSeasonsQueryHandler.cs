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

namespace Application.Features.SeasonFeatures.Queries.GetSeasons
{
    public class GetSeasonsQueryHandler : IRequestHandler<GetSeasonsQuery, OperationResult<List<SeasonDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSeasonsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<SeasonDto>>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
        {
            var seasonRepo = _unitOfWork.Repository<Season>();

            Expression<Func<Season, bool>>? filter = null;

            if (request.Year.HasValue && request.IsActive.HasValue)
            {
                filter = s => s.Year == request.Year.Value && s.IsActive == request.IsActive.Value;
            }
            else if (request.Year.HasValue)
            {
                filter = s => s.Year == request.Year.Value;
            }
            else if (request.IsActive.HasValue)
            {
                filter = s => s.IsActive == request.IsActive.Value;
            }

            var seasons = await seasonRepo.FindAllAsync(filter, cancellationToken);
            var dtos = _mapper.Map<List<SeasonDto>>(seasons);

            return OperationResult<List<SeasonDto>>.Success(dtos);
        }
    }

}
