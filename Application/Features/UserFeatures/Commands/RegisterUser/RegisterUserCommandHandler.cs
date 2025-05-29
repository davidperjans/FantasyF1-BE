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

namespace Application.Features.UserFeatures.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<RegisterUserResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<OperationResult<RegisterUserResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();

            var existingUser = await userRepo.FindAsync(u => u.Email.ToLower() == request.Email.ToLower());

            if (existingUser.Any())
                return OperationResult<RegisterUserResult>.Failure("User with this email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsEmailConfirmed = false
            };

            await userRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtService.GenerateJwtToken(user);
            var userDto = _mapper.Map<UserDto>(user);

            var result = new RegisterUserResult
            {
                Token = token,
                User = userDto
            };

            return OperationResult<RegisterUserResult>.Success(result);
        }
    }
}
