using Application.Common;
using Application.Features.UserFeatures.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.SearchUsers
{
    public class SearchUsersQuery : IRequest<OperationResult<List<UserDto>>>
    {
        public string Query { get; set; } = string.Empty;
    }
}
