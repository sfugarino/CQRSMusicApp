using Microsoft.EntityFrameworkCore;
using Music.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Artists.Any())
            {
                return;   // DB has been seeded
            }

            var authorId = Guid.NewGuid();

            var artist = new Artist
            {
                Id = authorId,
                Name = "Larry Sparks"
            };

            context.Artists.Add(artist);
            context.SaveChanges();
           
            var albumId = Guid.NewGuid();
            var album = new Album
            {
                Id = albumId,
                Title = "Silver Reflections",
                Artist = authorId,
                Genre = "Bluegrass",
                ReleaseDate = new DateTime(1981, 1, 1)
            };

            context.Albums.Add(album);
            context.SaveChanges();

            var song = new Song
            {
                Id = Guid.NewGuid(),
                Title = "Tennessee 1949",
                Artist = authorId,
                Album = albumId,
                Genre = "Bluegrass",
                ReleaseDate = new DateTime(1981, 1, 1)
            };

            context.Songs.Add(song);
            context.SaveChanges();
        }
    }
}
