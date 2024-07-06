using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Entities
{
    public class Album
    {
        public Album(Guid id, string title, Guid artist, string? genre, DateTime releaseDate)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Genre = genre;
            ReleaseDate = releaseDate;
        }

        public Album() { }

        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public Guid Artist { get; set; }
        public string? Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
