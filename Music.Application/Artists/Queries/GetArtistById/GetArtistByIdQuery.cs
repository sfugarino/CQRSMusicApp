using Music.Application.Abstractions.Messaging;
using Music.Application.Artists.Queries.GetAllArtists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Artists.Queries.GetArtistById
{
    public sealed record GetArtistByIdQuery(Guid Id) : IQuery<ArtistResponse>;

}
