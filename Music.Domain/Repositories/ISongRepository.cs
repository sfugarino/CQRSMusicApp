using Music.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Repositories
{
    public interface ISongRepository
    {
        Task<Song[]> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Song?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Song song);
        Task AddAsync(Song song);
        void Remove(Song song);
    }
}
