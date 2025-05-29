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

namespace Application.Features.UserFeatures.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<LoginUserResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<OperationResult<LoginUserResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();

            var user = await userRepo
                .FindAsync(u => u.Email.ToLower() == request.Email.ToLower());

            var foundUser = user.FirstOrDefault();
            if (foundUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, foundUser.PasswordHash))
                return OperationResult<LoginUserResult>.Failure("Felaktig e-post eller lösenord");

            // Update last login time
            foundUser.LastLoginAt = DateTime.UtcNow;

            var token = _jwtService.GenerateJwtToken(foundUser);
            var userDto = _mapper.Map<UserDto>(foundUser);

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<LoginUserResult>.Success(new LoginUserResult
            {
                Token = token,
                User = userDto
            });
        }
    }
}
