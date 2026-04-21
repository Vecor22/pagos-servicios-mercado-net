using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mercado.BL.BE;
using Mercado.BL.BC;

namespace MercadoPL.GUI.Controllers
{
    public class PuestoController : Controller
    {
        PuestoBC puestoBC = new PuestoBC();
        SocioBC  socioBC  = new SocioBC();

        private void CargarSocios(int? idSeleccionado = null)
        {
            var socios = socioBC.listar();
            ViewBag.Socios = new SelectList(socios, "IdSocio", "Nombre", idSeleccionado);
        }

        // GET: /Puesto
        public ActionResult Index()
        {
            var lista = puestoBC.listar();
            return View(lista);
        }

        // GET: /Puesto/Create
        public ActionResult Create()
        {
            CargarSocios();
            return View(new PuestoBE { Socio = new SocioBE() });
        }

        // POST: /Puesto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PuestoBE puesto, int IdSocio)
        {
            try
            {
                puesto.Socio = new SocioBE { IdSocio = IdSocio };
                int nuevoId = puestoBC.insertar(puesto);
                TempData["Mensaje"] = $"Puesto registrado correctamente. ID: {nuevoId}";
                TempData["Tipo"]    = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al registrar: " + ex.Message;
                TempData["Tipo"]    = "danger";
                CargarSocios(IdSocio);
                return View(puesto);
            }
        }

        // GET: /Puesto/Edit/5
        public ActionResult Edit(int id)
        {
            var puesto = puestoBC.buscar(id);
            if (puesto == null) return NotFound();
            CargarSocios(puesto.Socio?.IdSocio);
            return View(puesto);
        }

        // POST: /Puesto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PuestoBE puesto, int IdSocio)
        {
            try
            {
                puesto.IdPuesto = id;
                puesto.Socio    = new SocioBE { IdSocio = IdSocio };
                bool ok = puestoBC.actualizar(puesto);
                TempData["Mensaje"] = ok ? "Puesto actualizado correctamente." : "No se pudo actualizar.";
                TempData["Tipo"]    = ok ? "success" : "warning";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al actualizar: " + ex.Message;
                TempData["Tipo"]    = "danger";
                CargarSocios(IdSocio);
                return View(puesto);
            }
        }

        // GET: /Puesto/Delete/5
        public ActionResult Delete(int id)
        {
            var puesto = puestoBC.buscar(id);
            if (puesto == null) return NotFound();
            return View(puesto);
        }

        // POST: /Puesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                puestoBC.eliminar(id);
                TempData["Mensaje"] = "Puesto eliminado correctamente.";
                TempData["Tipo"]    = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al eliminar: " + ex.Message;
                TempData["Tipo"]    = "danger";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
