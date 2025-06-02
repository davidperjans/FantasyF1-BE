using Application.Common;
using Application.Features.UserFeatures.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.CoreEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, OperationResult<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCurrentUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OperationResult<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();

            var user = userRepo.GetByIdAsync(request.CurrentUserId);
            if (user == null)
                return OperationResult<UserDto>.Failure("User not found");

            var mappedUser = _mapper.Map<UserDto>(user.Result);

            return OperationResult<UserDto>.Success(mappedUser);
        }
    }
}
