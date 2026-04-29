using Microsoft.AspNetCore.Mvc;

namespace Mercado.PL.GUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
