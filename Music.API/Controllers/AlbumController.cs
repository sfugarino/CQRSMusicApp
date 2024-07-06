using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.Persistence;
using Music.Domain.Entities;

namespace Music.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<AlbumController> _logger;

        public AlbumController(ApplicationDbContext context, ILogger<AlbumController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "Albums")]
        public IEnumerable<Album> Get()
        {
           return _context.Albums.ToList();
        }
    }
}
