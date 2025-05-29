using Application.Common;
using Application.Features.SeasonFeatures.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.CoreEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SeasonFeatures.Commands.UpdateSeason
{
    public class UpdateSeasonCommandHandler : IRequestHandler<UpdateSeasonCommand, OperationResult<SeasonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<SeasonDto>> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
        {
            var seasonRepo = _unitOfWork.Repository<Season>();
            var season = await seasonRepo.GetByIdAsync(request.Id);

            if (season is null)
                return OperationResult<SeasonDto>.Failure("Season not found.");

            if (request.EndDate <= request.StartDate)
                return OperationResult<SeasonDto>.Failure("End date must be after start date.");

            // Inactive all the other seasons if this should be active
            if (request.IsActive)
            {
                var allSeasons = await seasonRepo.FindAllAsync(cancellationToken: cancellationToken);
                foreach (var s in allSeasons.Where(s => s.IsActive && s.Id != request.Id))
                {
                    s.IsActive = false;
                }
            }

            season.Name = request.Name;
            season.StartDate = request.StartDate;
            season.EndDate = request.EndDate;
            season.IsActive = request.IsActive;
            season.IsCompleted = request.IsCompleted;

            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<SeasonDto>(season);
            return OperationResult<SeasonDto>.Success(dto);
        }

    }
}
