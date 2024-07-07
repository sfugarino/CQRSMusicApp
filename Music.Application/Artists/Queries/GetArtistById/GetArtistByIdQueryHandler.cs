using Music.Application.Abstractions.Messaging;
using Music.Application.Artists.Queries.GetAllArtists;
using Music.Domain.Repositories;
using Music.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Artists.Queries.GetArtistById
{

    internal sealed class GetArtistByIdQueryHandler
    : IQueryHandler<GetArtistByIdQuery, ArtistResponse>
    {
        private readonly IArtistRepository _artistRepository;
        public GetArtistByIdQueryHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<Result<ArtistResponse>> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetByIdAsync(request.Id, cancellationToken);

            if(artist is null)
            {
                return Result.Failure<ArtistResponse>(new Error("Artist.NotFound", $"Artist with ID {request.Id} not found."));
            }

            var response = new ArtistResponse(artist.Id, artist.Name);

            return response;
        }
    }
}
