using Music.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Artists.Queries.GetAllArtists
{
    public sealed record GetAllArtistsQuery() : IQuery<ArtistResponse[]>;
}
