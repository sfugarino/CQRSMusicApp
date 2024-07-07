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
    public sealed class ArtistRepository : IArtistRepository
    {
        private ApplicationDbContext _context;

        public ArtistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Artist[]> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<Artist>().ToArrayAsync(cancellationToken);
        }

        public async Task<Artist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Artist>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(Artist artist)
        {
            _context.Set<Artist>().AddAsync(artist);
        }

        public async Task AddAsync(Artist artist)
        {
            await _context.Set<Artist>().AddAsync(artist);
        }

        public void Remove(Artist artist)
        {
            _context.Set<Artist>().Remove(artist);
        }
    }
}