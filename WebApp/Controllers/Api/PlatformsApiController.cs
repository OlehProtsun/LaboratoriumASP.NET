using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Games;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsApiController : ControllerBase
    {
        private readonly GamesContext _context;

        public PlatformsApiController(GamesContext context)
        {
            _context = context;
        }

        // GET /api/PlatformsApi?filter=...
        [HttpGet]
        public async Task<IActionResult> GetPlatforms([FromQuery] string? filter)
        {
            var query = _context.Platforms.AsQueryable();

            // Якщо користувач ввів щось для фільтра
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p => p.PlatformName.Contains(filter));
            }

            // Повертаємо { Id, PlatformName }
            var platforms = await query
                .Select(p => new { p.Id, p.PlatformName })
                .ToListAsync();

            return Ok(platforms);
        }
    }
}