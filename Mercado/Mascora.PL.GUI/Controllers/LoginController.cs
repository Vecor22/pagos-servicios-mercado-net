using Microsoft.AspNetCore.Mvc;

namespace Mercado.PL.GUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            var request = new
            {
                username = username,
                password = password
            };

            var response = await httpClient.PostAsJsonAsync(
                "https://localhost:7110/api/auth/login",
                request
            );

            if (!response.IsSuccessStatusCode)
            {
                var mensaje = await response.Content.ReadAsStringAsync();
                ViewBag.Error = string.IsNullOrWhiteSpace(mensaje)
                    ? "Usuario o contraseña incorrectos."
                    : mensaje.Trim('"');
                return View();
            }

            var usuario = await response.Content.ReadFromJsonAsync<LoginResponse>();

            HttpContext.Session.SetString("UsuarioID", usuario.UsuarioID.ToString());
            HttpContext.Session.SetString("Username", usuario.Username);
            HttpContext.Session.SetString("NombreCompleto", usuario.NombreCompleto);
            HttpContext.Session.SetString("Rol", usuario.Rol);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        private class LoginResponse
        {
            public long UsuarioID { get; set; }
            public string Username { get; set; }
            public string NombreCompleto { get; set; }
            public string Rol { get; set; }
        }
    }
}
