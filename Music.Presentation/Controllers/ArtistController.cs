using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Music.Persistence;
using Music.Domain.Entities;
using MediatR;
using Music.Application.Artists.Queries.GetAllArtists;
using Music.Application.Artists.Queries.GetArtistById;

namespace Music.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ApiController
    {
        private readonly ILogger<ArtistController> _logger;

        public ArtistController(ISender sender, ILogger<ArtistController> logger)
            : base(sender)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
           var query = new GetAllArtistsQuery();

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value)
                : BadRequest(response.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetArtistByIdQuery(id);

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value)
                : NotFound(response.Error);
        }
    }
}
