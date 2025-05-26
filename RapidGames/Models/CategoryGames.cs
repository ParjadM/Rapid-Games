using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidGames.Models
{
    public class CategoryGames
    {
        [Key]
        public int CategoryGamesId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
