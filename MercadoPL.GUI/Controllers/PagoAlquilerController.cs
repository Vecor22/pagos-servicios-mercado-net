using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mercado.BL.BE;
using Mercado.BL.BC;

namespace MercadoPL.GUI.Controllers
{
    public class PagoAlquilerController : Controller
    {
        PagoAlquilerBC pagoBC  = new();
        PuestoBC       puestoBC = new();

        private void CargarPuestos(int? idSeleccionado = null)
        {
            var puestos = puestoBC.listar();
            ViewBag.Puestos = new SelectList(
                puestos.Select(p => new { p.IdPuesto, Descripcion = $"{p.Codigo} - {p.NombreSocio}" }),
                "IdPuesto", "Descripcion", idSeleccionado);
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

        private void CargarEstados(string? seleccionado = null)
        {
            var estados = new List<string> { "Pagado", "Pendiente", "Anulado" };
            ViewBag.Estados = new SelectList(estados, seleccionado);
        }

        // GET: /PagoAlquiler
        public ActionResult Index()
        {
            var lista = pagoBC.listar();
            return View(lista);
        }

        // GET: /PagoAlquiler/Create
        public ActionResult Create()
        {
            CargarPuestos();
            CargarMeses();
            CargarEstados("Pagado");
            return View(new PagoAlquilerBE
            {
                FechaPago = DateTime.Today,
                Anio      = DateTime.Today.Year,
                Estado    = "Pagado"
            });
        }

        // POST: /PagoAlquiler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoAlquilerBE pago)
        {
            try
            {
                int nuevoId = pagoBC.insertar(pago);
                TempData["Mensaje"] = $"Pago registrado correctamente. ID: {nuevoId}";
                TempData["Tipo"]    = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error: " + ex.Message;
                TempData["Tipo"]    = "danger";
                CargarPuestos(pago.IdPuesto);
                CargarMeses();
                CargarEstados(pago.Estado);
                return View(pago);
            }
        }

        // GET: /PagoAlquiler/Edit/5
        public ActionResult Edit(int id)
        {
            var pago = pagoBC.buscar(id);
            if (pago == null) return NotFound();
            CargarPuestos(pago.IdPuesto);
            CargarMeses();
            CargarEstados(pago.Estado);
            return View(pago);
        }

        // POST: /PagoAlquiler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PagoAlquilerBE pago)
        {
            try
            {
                pago.IdPago = id;
                bool ok = pagoBC.actualizar(pago);
                TempData["Mensaje"] = ok ? "Pago actualizado correctamente." : "No se pudo actualizar.";
                TempData["Tipo"]    = ok ? "success" : "warning";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error: " + ex.Message;
                TempData["Tipo"]    = "danger";
                CargarPuestos(pago.IdPuesto);
                CargarMeses();
                CargarEstados(pago.Estado);
                return View(pago);
            }
        }

        // GET: /PagoAlquiler/Delete/5
        public ActionResult Delete(int id)
        {
            var pago = pagoBC.buscar(id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        // POST: /PagoAlquiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                pagoBC.eliminar(id);
                TempData["Mensaje"] = "Pago eliminado correctamente.";
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
