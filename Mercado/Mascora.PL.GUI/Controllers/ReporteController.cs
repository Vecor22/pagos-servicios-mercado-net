using Mercado.PL.GUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mercado.PL.GUI.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ReporteModel reporteModel = new ReporteModel();

        public IActionResult ResumenDeudas()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResumenIngresosDia()
        {
            ViewBag.ReporteIngresos = reporteModel.ObtenerResumenIngresosDia();
            ViewBag.TituloReporteIngresos = "Resumen de Ingresos del Dia";
            return View("ResumenDeudas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResumenIngresosMes()
        {
            ViewBag.ReporteIngresos = reporteModel.ObtenerResumenIngresosMes();
            ViewBag.TituloReporteIngresos = "Resumen de Ingresos del Mes";
            return View("ResumenDeudas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResumenIngresosAnio()
        {
            ViewBag.ReporteIngresos = reporteModel.ObtenerResumenIngresosAnio();
            ViewBag.TituloReporteIngresos = "Resumen de Ingresos del Año";
            return View("ResumenDeudas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResumenIngresosEntreFechas(DateTime? fechaInicioIngresos, DateTime? fechaFinIngresos)
        {
            try
            {
                if (!fechaInicioIngresos.HasValue || !fechaFinIngresos.HasValue)
                    throw new Exception("Debe ingresar la fecha de inicio y la fecha fin para el reporte de ingresos.");

                ViewBag.FechaInicioIngresos = fechaInicioIngresos.Value.ToString("yyyy-MM-dd");
                ViewBag.FechaFinIngresos = fechaFinIngresos.Value.ToString("yyyy-MM-dd");
                ViewBag.ReporteIngresos = reporteModel.ObtenerResumenIngresosEntreFechas(fechaInicioIngresos.Value, fechaFinIngresos.Value);
                ViewBag.TituloReporteIngresos = "Resumen de Ingresos entre Fechas";

                return View("ResumenDeudas");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorIngresos = ex.Message;
                ViewBag.FechaInicioIngresos = fechaInicioIngresos?.ToString("yyyy-MM-dd");
                ViewBag.FechaFinIngresos = fechaFinIngresos?.ToString("yyyy-MM-dd");
                return View("ResumenDeudas");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResumenDeudas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                if (!fechaInicio.HasValue || !fechaFin.HasValue)
                    throw new Exception("Debe ingresar la fecha de inicio y la fecha fin.");

                ViewBag.FechaInicio = fechaInicio.Value.ToString("yyyy-MM-dd");
                ViewBag.FechaFin = fechaFin.Value.ToString("yyyy-MM-dd");

                var reporte = reporteModel.ResumenDeudasEntreFechas(fechaInicio.Value, fechaFin.Value);

                if (reporte == null)
                {
                    ViewBag.Error = "No hay resultados para el rango de fechas seleccionado.";
                    return View();
                }

                return View(reporte);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
                ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");
                return View();
            }
        }
    }
}
