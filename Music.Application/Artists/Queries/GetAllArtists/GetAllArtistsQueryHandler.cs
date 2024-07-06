using Music.Application.Abstractions.Messaging;
using Music.Domain.Repositories;
using Music.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Artists.Queries.GetAllArtists
{
    internal sealed class GetAllArtistsQueryHandler
        : IQueryHandler<GetAllArtistsQuery, ArtistResponse[]>
    {
        private readonly IArtistRepository _artistRepository;
        public GetAllArtistsQueryHandler(IArtistRepository artistRepository) 
        {
            _artistRepository = artistRepository;
        }

        public async Task<Result<ArtistResponse[]>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetAllAsync(cancellationToken);
            return artists.Select(artist => new ArtistResponse(artist.Id, artist.Name)).ToArray();
        }
    }
}
