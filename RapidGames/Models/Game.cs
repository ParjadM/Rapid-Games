using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RapidGames.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string ImgNumber { get; set; } = string.Empty;

        public List<CategoryGames> CategoryGames { get; set; } = new List<CategoryGames>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }

}
