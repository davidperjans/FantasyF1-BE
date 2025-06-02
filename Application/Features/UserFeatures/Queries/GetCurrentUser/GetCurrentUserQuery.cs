using Application.Common;
using Application.Features.UserFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<OperationResult<UserDto>>
    {
        public Guid CurrentUserId { get; set; }
        public GetCurrentUserQuery(Guid currentUserId)
        {
            CurrentUserId = currentUserId;
        }
    }
}
