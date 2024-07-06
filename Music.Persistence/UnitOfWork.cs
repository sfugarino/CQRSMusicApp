using Music.Domain.Repositories;

namespace Music.Persistence
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
