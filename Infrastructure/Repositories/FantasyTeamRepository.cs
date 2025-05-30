using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities.CoreEntities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FantasyTeamRepository : Repository<FantasyTeam>, IFantasyTeamRepository
    {
        private readonly AppDbContext _context;

        public FantasyTeamRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FantasyTeam?> GetTeamWithDetailsAsync(Guid teamId, CancellationToken cancellationToken = default)
        {
            return await _context.FantasyTeams
                .Include(t => t.TeamDrivers)
                    .ThenInclude(td => td.Driver)
                .Include(t => t.TeamConstructors)
                    .ThenInclude(tc => tc.Constructor)
                .FirstOrDefaultAsync(t => t.Id == teamId, cancellationToken);
        }
    }
}
