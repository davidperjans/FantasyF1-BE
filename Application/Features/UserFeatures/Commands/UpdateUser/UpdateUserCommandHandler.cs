using Application.Common;
using Application.Features.UserFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();
            var user = userRepo.GetByIdAsync(request.UserId);
            if (user == null)
                return OperationResult<bool>.Failure("User not found");

            if (!string.IsNullOrWhiteSpace(request.Dto.UserName))
                user.Result.UserName = request.Dto.UserName;

            if (!string.IsNullOrWhiteSpace(request.Dto.Email))
                user.Result.Email = request.Dto.Email;

            if (!string.IsNullOrWhiteSpace(request.Dto.ProfilePictureUrl))
                user.Result.ProfileImageUrl = request.Dto.ProfilePictureUrl;

            userRepo.Update(user.Result);
            user.Result.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success(true);
        }
    }
}
