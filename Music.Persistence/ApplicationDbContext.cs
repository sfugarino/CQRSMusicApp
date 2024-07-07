using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music.Domain.Entities;

namespace Music.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Console.WriteLine("ApplicationDbContext::ctor -> options: {0}", options);
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Artist)
                .WithMany(e => e.Albums)
                .HasForeignKey(e => e.ArtistId)
                .HasConstraintName("FK_Album_Artist");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Artist)
                .WithMany(e => e.Songs)
                .HasForeignKey(e => e.ArtistId)
                .HasConstraintName("FK_Song_Artist");

                entity.HasOne(e => e.Album)
                .WithMany(e => e.Songs)
                .HasForeignKey(e => e.AlbumId)
                .HasConstraintName("FK_Song_Album");
            });

        }
    }
}
