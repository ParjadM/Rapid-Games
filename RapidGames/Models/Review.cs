using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidGames.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        public string? ReviewText { get; set; }
        public int Rating { get; set; }
    }
}
