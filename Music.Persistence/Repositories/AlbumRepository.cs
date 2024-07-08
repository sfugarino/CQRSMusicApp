using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Entities;
using Music.Domain.Repositories;

namespace Music.Persistence.Repositories
{
    public sealed class AlbumRepository : IAlbumRepository
    {
        private ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Album[]> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<Album>().ToArrayAsync(cancellationToken);
        }


        public async Task<Album?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Album>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(Album album)
        {
            _context.Set<Album>().Add(album);
        }

        public async Task AddAsync(Album album)
        {
            await _context.Set<Album>().AddAsync(album);
        }

        public void Remove(Album album)
        {
            _context.Set<Album>().Remove(album);
        }
    }
}