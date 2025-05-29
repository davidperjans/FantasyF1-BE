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

namespace Application.Features.SeasonFeatures.Commands.CreateSeason
{
    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, OperationResult<CreateSeasonResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<CreateSeasonResult>> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {
            var seasonRepo = _unitOfWork.Repository<Season>();

            var exists = await seasonRepo.AnyAsync(s => s.Year == request.Year, cancellationToken);
            if (exists)
                return OperationResult<CreateSeasonResult>.Failure("A season with this year already exists.");

            if (request.EndDate <= request.StartDate)
                return OperationResult<CreateSeasonResult>.Failure("End date must be after start date.");

            var season = new Season
            {
                Id = Guid.NewGuid(),
                Year = request.Year,
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = false,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            await seasonRepo.AddAsync(season);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<SeasonDto>(season);
            return OperationResult<CreateSeasonResult>.Success(new CreateSeasonResult { Season = dto });
        }
    }

}
