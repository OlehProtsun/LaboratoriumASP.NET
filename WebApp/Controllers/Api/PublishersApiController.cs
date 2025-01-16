using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Games;

namespace WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersApiController : ControllerBase
    {
        private readonly GamesContext _context;
        public PublishersApiController(GamesContext context)
        {
            _context = context;
        }

        // GET: /api/PublishersApi?filter=...
        [HttpGet]
        public async Task<IActionResult> GetPublishers([FromQuery] string? filter)
        {
            var query = _context.Publishers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p => p.PublisherName.Contains(filter));
            }

            var publishers = await query
                .Select(p => new { p.Id, p.PublisherName })
                .ToListAsync();

            return Ok(publishers);
        }
    }
}