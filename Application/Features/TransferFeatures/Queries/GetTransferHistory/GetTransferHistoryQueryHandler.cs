using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.TransferFeatures.DTOs;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using MediatR;

namespace Application.Features.TransferFeatures.Queries.GetTransferHistory
{
    public class GetTransferHistoryQueryHandler : IRequestHandler<GetTransferHistoryQuery, OperationResult<List<TransferDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransferHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<List<TransferDto>>> Handle(GetTransferHistoryQuery request, CancellationToken cancellationToken)
        {
            var transferRepo = _unitOfWork.Repository<Transfer>();

            var transfers = await transferRepo.FindAllAsync(t => t.FantasyTeamId == request.FantasyTeamId, cancellationToken);

            var dtos = transfers
                .OrderByDescending(t => t.TransferDate)
                .Select(t => new TransferDto
                {
                    Id = t.Id,
                    FantasyTeamId = t.FantasyTeamId,
                    OutDriverId = t.OutDriverId,
                    InDriverId = t.InDriverId,
                    OutConstructorId = t.OutConstructorId,
                    InConstructorId = t.InConstructorId,
                    TransferDate = t.TransferDate
                })
                .ToList();

            return OperationResult<List<TransferDto>>.Success(dtos);
        }
    }
}
