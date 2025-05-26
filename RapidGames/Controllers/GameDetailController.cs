using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using RapidGames.Data;               
using RapidGames.Models;             
using System.Linq;                   
using System.Collections.Generic;    

public class GameDetailController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<GameDetailController> _logger; 

   
    public GameDetailController(ApplicationDbContext context, ILogger<GameDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult GameDetail(int id)
    {
        _logger.LogInformation($"Fetching details for GameId: {id}");

        
        var gameDetails = _context.Games.FirstOrDefault(g => g.GameId == id);

        if (gameDetails == null)
        {
            _logger.LogWarning($"Game with GameId: {id} not found.");
            return NotFound();
        }
        var categoriesForThisGame = _context.CategoryGames
                                        .Where(cg => cg.GameId == id)
                                        .Select(cg => cg.Category) 
                                        .ToList();
        _logger.LogInformation($"Found {categoriesForThisGame.Count} categories for GameId: {id}");


        var reviewsForThisGame = _context.Review?
                                     .Where(r => r.GameId == id)
                                     .ToList() ?? new List<Review>();

        var displayModel = new
        {
            GameId = gameDetails.GameId,
            Title = gameDetails.Title,
            ImgNumber = gameDetails.ImgNumber,
            Developer = gameDetails.Developer,     
            ReleaseDate = gameDetails.ReleaseDate, 
            ReviewText = reviewsForThisGame.FirstOrDefault()?.ReviewText ?? "No review available",
            Rating = reviewsForThisGame.FirstOrDefault()?.Rating ?? 0,
            GameCategories = categoriesForThisGame 
        };

        return View(displayModel);
    }
}