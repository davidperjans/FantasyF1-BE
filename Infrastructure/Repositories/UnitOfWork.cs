using Application.Interfaces;
using Domain.Entities.CoreEntities;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        // Specialiserade repositories
        private IFantasyTeamRepository? _fantasyTeamRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);

            // Specialfall för FantasyTeam
            if (type == typeof(FantasyTeam))
            {
                if (_fantasyTeamRepository == null)
                {
                    _fantasyTeamRepository = new FantasyTeamRepository(_context);
                    _repositories[type] = _fantasyTeamRepository;
                }
                return (IRepository<T>)_repositories[type];
            }

            // Vanligt generiskt repository
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new Repository<T>(_context);
                _repositories[type] = repositoryInstance;
            }

            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
