using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RapidGames.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<CategoryGames> CategoryGames { get; set; }
    }
}
