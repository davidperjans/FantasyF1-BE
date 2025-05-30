using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;

namespace Application.Interfaces
{
    public interface IFantasyTeamRepository : IRepository<FantasyTeam>
    {
        Task<FantasyTeam?> GetTeamWithDetailsAsync(Guid teamId, CancellationToken cancellationToken = default);
    }
}
