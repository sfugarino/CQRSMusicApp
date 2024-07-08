using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Music.Domain.Entities;
using Music.Domain.Repositories;
using Newtonsoft.Json;

namespace Music.Persistence.Repositories
{
    public sealed class ArtistRepository : IArtistRepository
    {
        private ApplicationDbContext _context;
        private IDistributedCache _cache;

        public ArtistRepository(ApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Artist[]> GetAllAsync(CancellationToken cancellationToken)
        {
            var res = await _cache.GetStringAsync("artists", cancellationToken);
            var cachedArtists = string.IsNullOrEmpty(res) ? null : JsonConvert.DeserializeObject<Artist[]>(res);
            if (cachedArtists != null)
            {
                return cachedArtists;
            }

            var artists = await _context.Set<Artist>()
                .Include(e => e.Albums!)
                .ThenInclude(e => e.Songs)
                .ToArrayAsync(cancellationToken);

            await _cache.SetStringAsync("artists", 
                JsonConvert.SerializeObject(artists), 
                new DistributedCacheEntryOptions
                {
                 
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15)
                },
                cancellationToken);


            return artists;
        }

        public async Task<Artist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Artist>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(Artist artist)
        {
            _cache.Remove("artists");
            _context.Set<Artist>().AddAsync(artist);
        }

        public async Task AddAsync(Artist artist, CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync("artists");
            await _context.Set<Artist>().AddAsync(artist, cancellationToken);
        }

        public void Remove(Artist artist)
        {
            _cache.Remove("artists");
            _context.Set<Artist>().Remove(artist);
        }
    }
}