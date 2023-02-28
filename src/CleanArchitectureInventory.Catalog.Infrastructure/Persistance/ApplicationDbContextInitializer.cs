using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureInventory.Catalog.Infrastructure.Persistance
{
    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(
             ILogger<ApplicationDbContextInitializer> logger,
             ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task InitializeAsyn()
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
                _logger.LogError(ex, "Error occured while initilizing the database");
                throw;
            }
           

        }
        //use this if you want to seed default data

        //public async Task SeedAsync()
        //{
        //    try
        //    {
        //        await TrySeedAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public async Task TrySeedAsync()
        //{
            
           
        //    ///:TODO seed default data here if required
        //}
    }
}

