using Microsoft.AspNetCore.Mvc;
using Mercado.BL.BE;
using Mercado.BL.BC;

namespace MercadoPL.GUI.Controllers
{
    public class SocioController : Controller
    {
        SocioBC socioBC = new SocioBC();

        // GET: /Socio
        public ActionResult Index()
        {
            var lista = socioBC.listar();
            return View(lista);
        }

        // GET: /Socio/Create
        public ActionResult Create()
        {
            return View(new SocioBE());
        }

        // POST: /Socio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SocioBE socio)
        {
            try
            {
                int nuevoId = socioBC.insertar(socio);
                TempData["Mensaje"] = $"Socio registrado correctamente. ID: {nuevoId}";
                TempData["Tipo"]    = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al registrar: " + ex.Message;
                TempData["Tipo"]    = "danger";
                return View(socio);
            }
        }

        // GET: /Socio/Edit/5
        public ActionResult Edit(int id)
        {
            var socio = socioBC.buscar(id);
            if (socio == null) return NotFound();
            return View(socio);
        }

        // POST: /Socio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SocioBE socio)
        {
            try
            {
                socio.IdSocio = id;
                bool ok = socioBC.actualizar(socio);
                TempData["Mensaje"] = ok ? "Socio actualizado correctamente." : "No se pudo actualizar.";
                TempData["Tipo"]    = ok ? "success" : "warning";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al actualizar: " + ex.Message;
                TempData["Tipo"]    = "danger";
                return View(socio);
            }
        }

        // GET: /Socio/Delete/5
        public ActionResult Delete(int id)
        {
            var socio = socioBC.buscar(id);
            if (socio == null) return NotFound();
            return View(socio);
        }

        // POST: /Socio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                socioBC.eliminar(id);
                TempData["Mensaje"] = "Socio eliminado correctamente.";
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
