using Application.Common;
using Application.Features.DriverFeatures.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.CoreEntities;
using Application.Common.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DriverFeatures.Queries.GetDrivers
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, OperationResult<List<DriverDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDriversQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<DriverDto>>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            var driverRepo = _unitOfWork.Repository<Driver>();

            Expression<Func<Driver, bool>>? filter = null;

            if (!string.IsNullOrEmpty(request.Search))
            {
                filter = d =>
                    d.FirstName.Contains(request.Search) ||
                    d.LastName.Contains(request.Search) ||
                    d.Code.Contains(request.Search);
            }

            if (request.IsActive.HasValue)
            {
                filter = filter is null
                    ? d => d.IsActive == request.IsActive.Value
                    : filter.AndAlso(d => d.IsActive == request.IsActive.Value);
            }

            var drivers = await driverRepo.FindAllAsync(filter, cancellationToken);
            var dtos = _mapper.Map<List<DriverDto>>(drivers);

            return OperationResult<List<DriverDto>>.Success(dtos);
        }
    }
}
