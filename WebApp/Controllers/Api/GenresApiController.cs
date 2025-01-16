using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Games;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresApiController : ControllerBase
    {
        private readonly GamesContext _context;

        public GenresApiController(GamesContext context)
        {
            _context = context;
        }

        // GET: /api/GenresApi?filter=...
        [HttpGet]
        public async Task<IActionResult> GetGenres([FromQuery] string? filter)
        {
            var query = _context.Genres.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(g => g.GenreName.Contains(filter));
            }

            var results = await query
                .Select(g => new { g.Id, g.GenreName })
                .ToListAsync();

            return Ok(results);
        }
    }
}