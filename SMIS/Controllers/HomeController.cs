using Microsoft.AspNetCore.Mvc;

namespace SeeTech.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
