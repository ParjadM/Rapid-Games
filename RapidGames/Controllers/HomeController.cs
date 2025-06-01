using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RapidGames.Data;         
using RapidGames.Models;       
using System;
using System.Collections.Generic; 
using System.Diagnostics;
using System.Linq;              

namespace RapidGames.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home/Index action started: Attempting to fetch game list.");

            List<Game> games = new List<Game>();
            List<Review> reviews = new List<Review>();
            List<object> mergedList = new List<object>();

            try
            {
                if (_context != null && _context.Games != null)
                {
                    games = _context.Games.ToList();
                }
                else
                {
                    _logger.LogWarning("_context or _context.Games is null. Cannot fetch games.");
                }

                if (_context != null && _context.Review != null)
                {
                    reviews = _context.Review.ToList(); 
                }
                else
                {
                    _logger.LogWarning("_context or _context.Review is null. Cannot fetch reviews.");
                }

                _logger.LogInformation($"Raw games count from DB: {games.Count}, Raw reviews count from DB: {reviews.Count}");


                mergedList = games.Select(game => new
                {
                    ImgNumber = game.ImgNumber,
                    Title = game.Title,
                    GameId = game.GameId,
                    ReviewText = reviews.FirstOrDefault(r => r.GameId == game.GameId)?.ReviewText ?? "No review available",
                    Rating = reviews.FirstOrDefault(r => r.GameId == game.GameId)?.Rating ?? 0
                }).Cast<object>().ToList();

                _logger.LogInformation($"Final merged list count being sent to view: {mergedList.Count}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in Home/Index action while fetching or processing game data.");
            }

            return View(mergedList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}