using Music.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Repositories
{
    public interface IAlbumRepository
    {
        Task<Album?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Album album);
    }
}
