using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Entities
{
    public class Song
    {
        public Song(Guid id, string title, Guid artist, Guid? album, string? genre, DateTime? releaseDate)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Album = album;
            Genre = genre;
            ReleaseDate = releaseDate;
        }

        public Song() { }

        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public Guid Artist { get; set; }
        public Guid? Album { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
