using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Added for ILogger
using RapidGames.Data;          // Your DbContext namespace
using RapidGames.Models;        // Your Models namespace
using System;
using System.Collections.Generic; // Added for List<T>
using System.Diagnostics;
using System.Linq;              // Added for ToList() and FirstOrDefault()

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

        // Index() method now fetches and passes game data to Views/Home/Index.cshtml
        public IActionResult Index()
        {
            _logger.LogInformation("Home/Index action started: Attempting to fetch game list.");

            List<Game> games = new List<Game>();
            List<Review> reviews = new List<Review>();
            List<object> mergedList = new List<object>(); // Using List<object> for anonymous types

            try
            {
                // Ensure _context and _context.Games are not null before calling ToList()
                if (_context != null && _context.Games != null)
                {
                    games = _context.Games.ToList(); // Fetch all games
                }
                else
                {
                    _logger.LogWarning("_context or _context.Games is null. Cannot fetch games.");
                }

                if (_context != null && _context.Review != null)
                {
                    reviews = _context.Review.ToList(); // Fetch all reviews
                }
                else
                {
                    _logger.LogWarning("_context or _context.Review is null. Cannot fetch reviews.");
                }


                Console.WriteLine($"Total games retrieved: {games.Count}"); // For quick debug view
                Console.WriteLine($"Total reviews retrieved: {reviews.Count}");
                _logger.LogInformation($"Raw games count from DB: {games.Count}, Raw reviews count from DB: {reviews.Count}");

                // Create the merged list for the view
                mergedList = games.Select(game => new
                {
                    ImgNumber = game.ImgNumber,
                    Title = game.Title,
                    GameId = game.GameId,
                    // Ensure ReviewText and Rating properties exist on your Review model
                    ReviewText = reviews.FirstOrDefault(r => r.GameId == game.GameId)?.ReviewText ?? "No review available",
                    Rating = reviews.FirstOrDefault(r => r.GameId == game.GameId)?.Rating ?? 0
                }).Cast<object>().ToList(); // Cast to object for List<object>

                Console.WriteLine($"Merged list count: {mergedList.Count}");
                _logger.LogInformation($"Final merged list count being sent to view: {mergedList.Count}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in Home/Index action while fetching or processing game data.");
                // Optionally, you could pass an error message to the view or return a specific error view
                // For now, it will fall through and return the (possibly empty) mergedList
            }

            // This now passes the mergedList to your Views/Home/Index.cshtml
            return View(mergedList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // The old GameList() method is no longer the primary way to show the main game list
        // if Index.cshtml is your game list view. You can remove this method,
        // comment it out, or repurpose it if you have a different /Home/GameList route/view.
        /*
        public IActionResult GameList() 
        {
            var games = _context.Games?.ToList() ?? new List<Game>();
            var reviews = _context.Review?.ToList() ?? new List<Review>();

            Console.WriteLine($"Total games retrieved: {games.Count}");
            Console.WriteLine($"Total reviews retrieved: {reviews.Count}");

            var mergedList = games.Select(game => new
            {
                ImgNumber = game.ImgNumber,
                Title = game.Title,
                GameId = game.GameId,
                ReviewText = reviews.FirstOrDefault(r => r.GameId == game.GameId)?.ReviewText ?? "No review available",
                Rating = reviews.FirstOrDefault(r => r.GameId == game.GameId)?.Rating ?? 0
            }).ToList();

            Console.WriteLine($"Merged list count: {mergedList.Count}");
            return View(mergedList);
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}