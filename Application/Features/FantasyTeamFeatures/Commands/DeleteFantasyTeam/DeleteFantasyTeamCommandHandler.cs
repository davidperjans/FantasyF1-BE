using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using MediatR;

namespace Application.Features.FantasyTeamFeatures.Commands.DeleteFantasyTeam
{
    public class DeleteFantasyTeamCommandHandler : IRequestHandler<DeleteFantasyTeamCommand, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFantasyTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<FantasyTeam>();
            var team = await repo.GetByIdAsync(request.FantasyTeamId);

            if (team is null)
                return OperationResult<bool>.Failure("Fantasy team not found.");

            if (team.UserId != request.UserId)
                return OperationResult<bool>.Failure("You do not have permission to delete this team.");

            repo.Remove(team);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success(true);
        }
    }
}
