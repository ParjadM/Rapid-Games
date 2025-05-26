using Microsoft.AspNetCore.Mvc;

namespace RapidGames.Controllers
{
    public class GameDetailController : Controller
    {
        public IActionResult GameDetail()
        {
            return View();
        }
    }
}
