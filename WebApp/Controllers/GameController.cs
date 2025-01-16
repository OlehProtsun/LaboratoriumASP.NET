using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Games;


namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        private readonly GamesContext _context;

        public GameController(GamesContext context)
        {
            _context = context;
        }

        // GET: Game
        
        //[Authorize]
        public async Task<IActionResult> Index(int page = 1, int size = 20)
        {
            var totalCount = await _context.Games.CountAsync();
            var pagingList = PagingListAsync<Game>.Create(
                (p, s) => _context.Games
                    .Include(g => g.Genre)
                    .Include(g => g.GamePublishers)
                    .ThenInclude(gp => gp.Publisher)
                    .OrderBy(g => g.GameName)
                    .Skip((p - 1) * s)
                    .Take(s)
                    .AsAsyncEnumerable(),
                totalCount,
                page,
                size);

            return View(pagingList);
        }

        // GET: Game/Details
        
        //[Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.GamePublishers)
                .ThenInclude(gp => gp.Publisher)
                .Include(g => g.GamePublishers)
                .ThenInclude(gp => gp.GamePlatforms)
                .ThenInclude(gpl => gpl.Platform)
                .Include(g => g.GamePublishers)
                .ThenInclude(gp => gp.GamePlatforms)
                .ThenInclude(gpl => gpl.RegionSales)
                .ThenInclude(rs => rs.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null) return NotFound();
            var viewModel = new GameDetailsViewModel
            {
                GameId   = game.Id,
                GameName = game.GameName,
                GenreName = game.Genre?.GenreName ?? "No Genre",
                Publishers = game.GamePublishers.Select(gp => new PublisherInfo
                {
                    PublisherId   = gp.PublisherId,
                    PublisherName = gp.Publisher?.PublisherName ?? "Unknown publisher",
                    Platforms = gp.GamePlatforms.Select(gpl => new PlatformInfo
                    {
                        PlatformName = gpl.Platform?.PlatformName ?? "Unknown platform",
                        ReleaseYear  = gpl.ReleaseYear,
                        RegionSales  = gpl.RegionSales.Select(rs => new RegionSaleInfo
                        {
                            RegionName = rs.Region?.RegionName ?? "Unknown region",
                            NumSales   = rs.NumSales ?? 0
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
            return View(viewModel);
        }


        // GET: Game/Create
        
        //[Authorize]
        public IActionResult Create()
        {
            return View();
        }

        
        // POST: Game/Create
        
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameModel model)
        {
            if (ModelState.IsValid)
            {
                var newGame = new Game
                {
                    Id = _context.Games.Any() ? _context.Games.Max(g => g.Id) + 1 : 1,
                    GameName = model.GameName,
                    GenreId = model.GenreId
                };
                _context.Games.Add(newGame);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        
        // GET: Game/Edit
        
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", game.GenreId);
            return View(game);
        }


        // POST: Game/Edit
        
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenreId,GameName")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);  
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", game.GenreId);
            return View(game);
        }


        // GET: Game/Delete
        
        //[Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var game = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.GamePublishers)
                .ThenInclude(gp => gp.Publisher)
                .Include(g => g.GamePublishers)
                .ThenInclude(gp => gp.GamePlatforms)
                .ThenInclude(gpl => gpl.Platform)
                .Include(g => g.GamePublishers)
                .ThenInclude(gp => gp.GamePlatforms)
                .ThenInclude(gpl => gpl.RegionSales)
                .ThenInclude(rs => rs.Region)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game); 
        }


        // POST: Game/Delete
        
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
