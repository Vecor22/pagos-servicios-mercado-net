using Mercado.BL.BE;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Filters;
using Mercado.PL.GUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mercado.PL.GUI.Controllers
{
    [RolAutorizado("ADMINISTRADOR")]
    public class RolController : Controller
    {
        private readonly RolModel rolModel = new RolModel();

        public IActionResult Index()
        {
            var roles = rolModel.Listar()
                .Select(r => new RolResponse
                {
                    RolID = r.RolID,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    Activo = r.Activo,
                    FechaCreacion = r.FechaCreacion,
                    FechaActualizacion = r.FechaActualizacion
                })
                .ToList();

            return View(roles);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(RolRequest request)
        {
            try
            {
                RolBE rol = new RolBE
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion
                };

                rolModel.Crear(rol);

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
            var rol = rolModel.BuscarPorId(id);

            if (rol == null)
                return NotFound();

            var request = new RolRequest
            {
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            };

            ViewBag.RolID = rol.RolID;
            ViewBag.Activo = rol.Activo;

            return View(request);
        }

        [HttpPost]
        public IActionResult Editar(long id, RolRequest request, bool activo)
        {
            try
            {
                RolBE rol = new RolBE
                {
                    RolID = id,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    Activo = activo
                };

                rolModel.Actualizar(rol);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.RolID = id;
                ViewBag.Activo = activo;
                return View(request);
            }
        }
    }
}