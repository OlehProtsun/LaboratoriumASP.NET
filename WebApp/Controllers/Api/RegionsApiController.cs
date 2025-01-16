using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Games;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsApiController : ControllerBase
    {
        private readonly GamesContext _context;

        public RegionsApiController(GamesContext context)
        {
            _context = context;
        }

        // GET /api/RegionsApi?filter=...
        [HttpGet]
        public async Task<IActionResult> GetRegions([FromQuery] string? filter)
        {
            var query = _context.Regions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(r => r.RegionName.Contains(filter));
            }

            var regions = await query
                .Select(r => new { r.Id, r.RegionName })
                .ToListAsync();

            return Ok(regions);
        }
    }
}