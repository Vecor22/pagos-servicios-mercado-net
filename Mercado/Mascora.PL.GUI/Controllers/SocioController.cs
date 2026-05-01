using Microsoft.AspNetCore.Mvc;
using Mercado.BL.BE;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;

namespace Mercado.PL.GUI.Controllers
{
    public class SocioController : Controller
    {
        private readonly SocioModel socioModel = new SocioModel();

        public IActionResult Index()
        {
            var socios = socioModel.Listar()
                .Select(s => new SocioResponse
                {
                    SocioID = s.SocioID,
                    CodigoSocio = s.CodigoSocio,
                    NombreCompleto = $"{s.Nombres} {s.Apellidos}",
                    Dni = s.Dni,
                    Correo = s.Correo,
                    Telefono = s.Telefono,
                    Estado = s.Estado,
                    FechaCreacion = s.FechaCreacion,
                    FechaActualizacion = s.FechaActualizacion
                })
                .ToList();

            return View(socios);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(SocioRequest request)
        {
            try
            {
                SocioBE socio = new SocioBE
                {
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Dni = request.Dni,
                    Correo = request.Correo,
                    Telefono = request.Telefono
                };

                socioModel.Crear(socio);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(request);
            }
        }

        public IActionResult Editar(long id)
        {
            var socio = socioModel.BuscarPorId(id);

            if (socio == null)
                return NotFound();

            var request = new SocioRequest
            {
                Nombres = socio.Nombres,
                Apellidos = socio.Apellidos,
                Dni = socio.Dni,
                Correo = socio.Correo,
                Telefono = socio.Telefono
            };

            ViewBag.SocioID = socio.SocioID;
            ViewBag.Estado = socio.Estado;

            return View(request);
        }

        [HttpPost]
        public IActionResult Editar(long id, SocioRequest request)
        {
            try
            {
                var socioActual = socioModel.BuscarPorId(id);

                if (socioActual == null)
                    return NotFound();

                SocioBE socio = new SocioBE
                {
                    SocioID = id,
                    CodigoSocio = socioActual.CodigoSocio,
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Dni = request.Dni,
                    Correo = request.Correo,
                    Telefono = request.Telefono,
                    Estado = socioActual.Estado
                };

                socioModel.Actualizar(socio);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.SocioID = id;

                var socioActual = socioModel.BuscarPorId(id);
                ViewBag.Estado = socioActual?.Estado;

                return View(request);
            }
        }

        public IActionResult CambiarEstado(long id, string estado)
        {
            socioModel.CambiarEstado(id, estado);
            return RedirectToAction("Index");
        }
    }
}
