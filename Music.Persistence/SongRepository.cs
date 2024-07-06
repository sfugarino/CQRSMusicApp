using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Entities;
using Music.Domain.Repositories;

namespace Music.Persistence
{
    public sealed class SongRepository : ISongRepository
    {
        private ApplicationDbContext _context;

        public SongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Song?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Song>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(Song song)
        {
           _context.Set<Song>().Add(song);
        }

        public void Remove(Song song)
        {
            _context.Set<Song>().Remove(song);
        }
    }
}
