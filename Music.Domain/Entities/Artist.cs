using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Entities
{
    public class Artist
    {
        public Artist(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Artist() { }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;

        public ICollection<Album>? Albums { get; }
        public ICollection<Song>? Songs { get; }

    }
}