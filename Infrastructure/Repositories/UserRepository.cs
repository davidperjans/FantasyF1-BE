using Application.Interfaces;
using Domain.Entities.CoreEntities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> GetByUsernameOrEmailAsync(string query)
        {
            return await _context.Users
                .Where(u => u.UserName.ToLower() == query.ToLower() || u.Email.ToLower() == query.ToLower())
                .ToListAsync();
        }
    }
}
