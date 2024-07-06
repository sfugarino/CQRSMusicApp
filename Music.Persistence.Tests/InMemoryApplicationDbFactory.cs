using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Tests
{
    public class InMemoryApplicationDbFactory : IDbContextFactory<ApplicationDbContext>
    {
        private readonly ApplicationDbContext _dbContext;

        public InMemoryApplicationDbFactory(string databaseName = "InMemoryTest")
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
            _dbContext = new ApplicationDbContext(options);
        }

        public ApplicationDbContext CreateDbContext()
        {
            return _dbContext;
        }
    }
}
