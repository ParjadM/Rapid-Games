using Microsoft.AspNetCore.Mvc;

namespace RapidGames.Models
{
    public class GameReviewViewModel
    {
        public List<Game>? Games { get; set; }
        public List<Review>? Review { get; set; }
    }

}
