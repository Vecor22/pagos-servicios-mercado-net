using Microsoft.AspNetCore.Mvc;

namespace MercadoPL.GUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
