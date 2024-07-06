using System;
using System.Collections.Generic;
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

        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
