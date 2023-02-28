using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureInventory.Receiving.Infrastructure.Persistance
{
    public class ApplicaitonDbContextInitializer
    {
        private readonly ILogger<ApplicaitonDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicaitonDbContextInitializer(ILogger<ApplicaitonDbContextInitializer> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                   await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Unable to migrate db.");
                throw;
            }
            
        }
    }
}

