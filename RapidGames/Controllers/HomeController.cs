using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RapidGames.Models;
using RapidGames.Data;

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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

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

            Console.WriteLine($"Merged list count: {mergedList.Count}"); // Final debug check

            return View(mergedList);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
