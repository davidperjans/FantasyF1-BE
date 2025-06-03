using Application.Common;
using Application.Features.UserFeatures.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.SearchUsers
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, OperationResult<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SearchUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<List<UserDto>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userRepository.GetByUsernameOrEmailAsync(request.Query);

            if (users.Result.Count == 0)
                return OperationResult<List<UserDto>>.Failure("No users found matching the query.");

            var dto = _mapper.Map<List<UserDto>>(users.Result);

            return OperationResult<List<UserDto>>.Success(dto);
        }
    }
}
