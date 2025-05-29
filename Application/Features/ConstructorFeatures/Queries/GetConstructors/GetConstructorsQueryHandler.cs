using Application.Common;
using Application.Common.Extensions;
using Application.Features.ConstructorFeatures.DTOs;
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

namespace Application.Features.ConstructorFeatures.Queries.GetConstructors
{
    public class GetConstructorsQueryHandler : IRequestHandler<GetConstructorsQuery, OperationResult<List<ConstructorDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetConstructorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<ConstructorDto>>> Handle(GetConstructorsQuery request, CancellationToken cancellationToken)
        {
            var constructorRepo = _unitOfWork.Repository<Constructor>();

            Expression<Func<Constructor, bool>>? filter = null;

            if (!string.IsNullOrEmpty(request.Search))
                filter = c => c.Name.Contains(request.Search);

            if (request.IsActive.HasValue)
                filter = filter is null
                    ? c => c.IsActive == request.IsActive.Value
                    : filter.AndAlso(c => c.IsActive == request.IsActive.Value);

            var constructors = await constructorRepo.FindAllAsync(filter, cancellationToken);
            var dtos = _mapper.Map<List<ConstructorDto>>(constructors);

            return OperationResult<List<ConstructorDto>>.Success(dtos);
        }
    }
}
