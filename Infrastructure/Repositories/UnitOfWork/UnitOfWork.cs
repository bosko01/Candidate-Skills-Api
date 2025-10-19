using Domain.Interfaces.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;

        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(AppDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
