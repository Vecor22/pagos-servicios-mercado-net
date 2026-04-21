using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mercado.BL.BE;
using Mercado.BL.BC;

namespace MercadoPL.GUI.Controllers
{
    public class MantenimientoController : Controller
    {
        MantenimientoBC manBC = new();

        private void CargarTipos(string? seleccionado = null)
        {
            var tipos = new List<string> { "Luz", "Agua", "Vigilancia", "Limpieza", "Internet", "Otro" };
            ViewBag.Tipos = new SelectList(tipos, seleccionado);
        }

        private void CargarMeses()
        {
            var meses = new List<string>
            {
                "Enero","Febrero","Marzo","Abril","Mayo","Junio",
                "Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"
            };
            ViewBag.Meses = new SelectList(meses);
        }

        // GET: /Mantenimiento
        public ActionResult Index()
        {
            var lista = manBC.listar();
            return View(lista);
        }

        // GET: /Mantenimiento/Create
        public ActionResult Create()
        {
            CargarTipos();
            CargarMeses();
            return View(new MantenimientoBE
            {
                Fecha = DateTime.Today,
                Anio  = DateTime.Today.Year
            });
        }

        // POST: /Mantenimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientoBE m)
        {
            try
            {
                int nuevoId = manBC.insertar(m);
                TempData["Mensaje"] = $"Mantenimiento registrado correctamente. ID: {nuevoId}";
                TempData["Tipo"]    = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error: " + ex.Message;
                TempData["Tipo"]    = "danger";
                CargarTipos(m.Tipo);
                CargarMeses();
                return View(m);
            }
        }

        // GET: /Mantenimiento/Edit/5
        public ActionResult Edit(int id)
        {
            var m = manBC.buscar(id);
            if (m == null) return NotFound();
            CargarTipos(m.Tipo);
            CargarMeses();
            return View(m);
        }

        // POST: /Mantenimiento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MantenimientoBE m)
        {
            try
            {
                m.IdMantenimiento = id;
                bool ok = manBC.actualizar(m);
                TempData["Mensaje"] = ok ? "Mantenimiento actualizado correctamente." : "No se pudo actualizar.";
                TempData["Tipo"]    = ok ? "success" : "warning";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error: " + ex.Message;
                TempData["Tipo"]    = "danger";
                CargarTipos(m.Tipo);
                CargarMeses();
                return View(m);
            }
        }

        // GET: /Mantenimiento/Delete/5
        public ActionResult Delete(int id)
        {
            var m = manBC.buscar(id);
            if (m == null) return NotFound();
            return View(m);
        }

        // POST: /Mantenimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                manBC.eliminar(id);
                TempData["Mensaje"] = "Mantenimiento eliminado correctamente.";
                TempData["Tipo"]    = "success";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error: " + ex.Message;
                TempData["Tipo"]    = "danger";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
