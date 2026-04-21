using Microsoft.AspNetCore.Mvc;
using Mercado.BL.BC;

namespace MercadoPL.GUI.Controllers
{
    public class ReporteController : Controller
    {
        PagoAlquilerBC  pagoBC = new();
        MantenimientoBC manBC  = new();
        PuestoBC        puestoBC = new();

        // GET: /Reporte
        public ActionResult Index()
        {
            ViewBag.Anio = DateTime.Today.Year;
            return View();
        }

        // GET: /Reporte/PagosPorAnio?anio=2025
        // Reporte 1: Pagos de alquiler por año - muestra totales por mes y por puesto
        public ActionResult PagosPorAnio(int anio = 0)
        {
            if (anio == 0) anio = DateTime.Today.Year;

            var pagos = pagoBC.listarPorAnio(anio);

            // Regla de negocio: agrupar por mes y calcular totales
            var resumenPorMes = pagos
                .GroupBy(p => p.Mes)
                .Select(g => new
                {
                    Mes        = g.Key,
                    Total      = g.Sum(x => x.Monto),
                    Cantidad   = g.Count(),
                    Pagados    = g.Count(x => x.Estado == "Pagado"),
                    Pendientes = g.Count(x => x.Estado == "Pendiente")
                })
                .ToArray();

            var totalGeneral  = pagos.Sum(p => p.Monto);
            var totalPagados  = pagos.Where(p => p.Estado == "Pagado").Sum(p => p.Monto);
            var totalPendiente= pagos.Where(p => p.Estado == "Pendiente").Sum(p => p.Monto);

            ViewBag.Anio          = anio;
            ViewBag.Pagos         = pagos;
            ViewBag.ResumenPorMes = resumenPorMes;
            ViewBag.TotalGeneral  = totalGeneral;
            ViewBag.TotalPagados  = totalPagados;
            ViewBag.TotalPendiente= totalPendiente;
            ViewBag.CantidadTotal = pagos.Count;

            return View();
        }

        // GET: /Reporte/GastosMantenimiento?anio=2025
        // Reporte 2: Gastos de mantenimiento por año - agrupado por tipo (luz, agua, etc.)
        public ActionResult GastosMantenimiento(int anio = 0)
        {
            if (anio == 0) anio = DateTime.Today.Year;

            var gastos = manBC.listarPorAnio(anio);

            // Regla de negocio: agrupar por tipo de servicio
            var resumenPorTipo = gastos
                .GroupBy(m => m.Tipo)
                .Select(g => new
                {
                    Tipo       = g.Key,
                    Total      = g.Sum(x => x.Monto),
                    Cantidad   = g.Count(),
                    Promedio   = g.Average(x => x.Monto)
                })
                .OrderByDescending(x => x.Total)
                .ToArray();

            var totalGastos   = gastos.Sum(m => m.Monto);
            var gastoMayor    = resumenPorTipo.FirstOrDefault();
            var totalPagosAnio= pagoBC.listarPorAnio(anio).Sum(p => p.Monto);
            var balance       = totalPagosAnio - totalGastos;

            ViewBag.Anio           = anio;
            ViewBag.Gastos         = gastos;
            ViewBag.ResumenPorTipo = resumenPorTipo;
            ViewBag.TotalGastos    = totalGastos;
            ViewBag.GastoMayor     = gastoMayor;
            ViewBag.TotalIngresos  = totalPagosAnio;
            ViewBag.Balance        = balance;

            return View();
        }
    }
}
