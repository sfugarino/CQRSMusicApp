using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Music.Domain.Entities
{
    public class Song
    {
        public Song(Guid id, string title, Guid artist, Guid? album, string? genre, DateTime? releaseDate)
        {
            Id = id;
            Title = title;
            ArtistId = artist;
            AlbumId = album;
            Genre = genre;
            ReleaseDate = releaseDate;
        }

        public Song() { }

        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }


    }
}
