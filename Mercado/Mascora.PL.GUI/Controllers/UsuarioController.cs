using Mercado.BL.BE;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Filters;
using Mercado.PL.GUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Mercado.PL.GUI.Controllers
{
    [RolAutorizado("ADMINISTRADOR")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioModel usuarioModel = new UsuarioModel();
        private readonly RolModel rolModel = new RolModel();
        private readonly IWebHostEnvironment webHostEnvironment;

        public UsuarioController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var usuarios = usuarioModel.Listar()
                .Select(u => new UsuarioResponse
                {
                    UsuarioID = u.UsuarioID,
                    Username = u.Username,
                    NombreCompleto = u.NombreCompleto,
                    FotoUrl = u.FotoUrl,
                    Estado = u.Estado,
                    FechaCreacion = u.FechaCreacion,
                    FechaActualizacion = u.FechaActualizacion,
                    Rol = u.Rol.Nombre
                })
                .ToList();

            return View(usuarios);
        }

        public IActionResult Crear()
        {
            ViewBag.Roles = rolModel.Listar();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(UsuarioRequest request)
        {
            try
            {
                request.FotoUrl = GuardarFoto(request.FotoArchivo);

                UsuarioBE usuario = new UsuarioBE
                {
                    Username = request.Username,
                    PasswordHash = HashPassword(request.Password),
                    NombreCompleto = request.NombreCompleto,
                    FotoUrl = request.FotoUrl,
                    Rol = new RolBE { RolID = request.RolID }
                };

                usuarioModel.Crear(usuario);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = rolModel.Listar();
                return View(request);
            }
        }

        public IActionResult Editar(long id)
        {
            var usuario = usuarioModel.BuscarPorId(id);

            if (usuario == null)
                return NotFound();

            var request = new UsuarioRequest
            {
                Username = usuario.Username,
                Password = "",
                NombreCompleto = usuario.NombreCompleto,
                FotoUrl = usuario.FotoUrl,
                RolID = usuario.Rol.RolID
            };

            ViewBag.UsuarioID = usuario.UsuarioID;
            ViewBag.Estado = usuario.Estado;
            ViewBag.Roles = rolModel.Listar();

            return View(request);
        }

        [HttpPost]
        public IActionResult Editar(long id, UsuarioRequest request)
        {
            try
            {
                var usuarioActual = usuarioModel.BuscarPorId(id);

                if (usuarioActual == null)
                    return NotFound();

                request.FotoUrl = request.FotoArchivo != null
                    ? GuardarFoto(request.FotoArchivo)
                    : usuarioActual.FotoUrl;

                UsuarioBE usuario = new UsuarioBE
                {
                    UsuarioID = id,
                    Username = request.Username,
                    NombreCompleto = request.NombreCompleto,
                    FotoUrl = request.FotoUrl,
                    Rol = new RolBE { RolID = request.RolID }
                };

                usuarioModel.Actualizar(usuario);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.UsuarioID = id;
                ViewBag.Roles = rolModel.Listar();
                return View(request);
            }
        }

        public IActionResult CambiarEstado(long id, string estado)
        {
            usuarioModel.CambiarEstado(id, estado);
            return RedirectToAction("Index");
        }

        private static string HashPassword(string password)
        {
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        private string? GuardarFoto(IFormFile? fotoArchivo)
        {
            if (fotoArchivo == null || fotoArchivo.Length == 0)
                return null;

            var extension = Path.GetExtension(fotoArchivo.FileName).ToLowerInvariant();
            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!extensionesPermitidas.Contains(extension))
                throw new Exception("La foto debe estar en formato JPG, JPEG, PNG o WEBP.");

            const long maxBytes = 5 * 1024 * 1024;

            if (fotoArchivo.Length > maxBytes)
                throw new Exception("La foto no puede superar los 5 MB.");

            var carpetaUploads = Path.Combine(webHostEnvironment.WebRootPath, "uploads", "usuarios");
            Directory.CreateDirectory(carpetaUploads);

            var nombreArchivo = $"{Guid.NewGuid():N}{extension}";
            var rutaArchivo = Path.Combine(carpetaUploads, nombreArchivo);

            using var stream = new FileStream(rutaArchivo, FileMode.Create);
            fotoArchivo.CopyTo(stream);

            return $"/uploads/usuarios/{nombreArchivo}";
        }
    }
}
