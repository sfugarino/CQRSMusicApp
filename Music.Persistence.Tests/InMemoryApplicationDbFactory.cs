using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Tests
{
    public class InMemoryApplicationDbFactory : IDbContextFactory<ApplicationDbContext>, IDisposable
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

        // To detect redundant calls
        private bool _disposedValue;

        ~InMemoryApplicationDbFactory() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }
    }
}
