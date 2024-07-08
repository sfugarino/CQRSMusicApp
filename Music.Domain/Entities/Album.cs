using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.Entities
{
    public class Album
    {
        public Album(Guid id, string title, Guid artist, string? genre, DateTime releaseDate)
        {
            Id = id;
            Title = title;
            ArtistId = artist;
            Genre = genre;
            ReleaseDate = releaseDate;
        }

        public Album() { }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public Guid ArtistId { get; set; }

        [JsonIgnore]
        public Artist? Artist { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public ICollection<Song>? Songs { get; }
    }
}
