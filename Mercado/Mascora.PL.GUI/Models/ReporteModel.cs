using Mercado.BL.BC;
using Mercado.PL.GUI.DTO.Response;
using System.Data;

namespace Mercado.PL.GUI.Models
{
    public class ReporteModel
    {
        private readonly ReporteBC reporteBC = new ReporteBC();

        public DataTable ResumenIngresosDia()
        {
            return reporteBC.ResumenIngresosDia();
        }

        public ReporteResponse? ObtenerResumenIngresosDia()
        {
            return MapearResumenIngresos(reporteBC.ResumenIngresosDia());
        }

        public DataTable ResumenIngresosMes()
        {
            return reporteBC.ResumenIngresosMes();
        }

        public ReporteResponse? ObtenerResumenIngresosMes()
        {
            return MapearResumenIngresos(reporteBC.ResumenIngresosMes());
        }

        public DataTable ResumenIngresosAnio()
        {
            return reporteBC.ResumenIngresosAnio();
        }

        public ReporteResponse? ObtenerResumenIngresosAnio()
        {
            return MapearResumenIngresos(reporteBC.ResumenIngresosAnio());
        }

        public DataTable ResumenIngresosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return reporteBC.ResumenIngresosEntreFechas(fechaInicio, fechaFin);
        }

        public ReporteResponse? ObtenerResumenIngresosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return MapearResumenIngresos(reporteBC.ResumenIngresosEntreFechas(fechaInicio, fechaFin));
        }

        public DataTable ResumenDeudas()
        {
            return reporteBC.ResumenDeudas();
        }

        public ReporteResumenDeudasResponse? ResumenDeudasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var tabla = reporteBC.ResumenDeudasEntreFechas(fechaInicio, fechaFin);

            if (tabla.Rows.Count == 0)
                return null;

            var fila = tabla.Rows[0];

            return new ReporteResumenDeudasResponse
            {
                TotalPagableMonto = ObtenerDecimal(fila, "total_pagable_monto", "total_pagable", "total_deuda"),
                TotalPagableCantidad = ObtenerInt(fila, "total_pagable_cantidad", "total_deudas_pagables", "total_deudas"),
                TotalPagablePorcentaje = ObtenerDecimal(fila, "total_pagable_porcentaje", "total_pagable_pct", "total_porcentaje", defaultValue: 100m),
                PendienteMonto = ObtenerDecimal(fila, "pendiente_monto", "total_pendiente"),
                PendienteCantidad = ObtenerInt(fila, "pendiente_cantidad", "cantidad_pendiente"),
                PendientePorcentaje = ObtenerDecimal(fila, "pendiente_porcentaje", "porcentaje_pendiente"),
                PagadaMonto = ObtenerDecimal(fila, "pagada_monto", "total_pagado"),
                PagadaCantidad = ObtenerInt(fila, "pagada_cantidad", "cantidad_pagada"),
                PagadaPorcentaje = ObtenerDecimal(fila, "pagada_porcentaje", "porcentaje_pagada"),
                ExoneradaCantidad = ObtenerInt(fila, "exonerada_cantidad", "cantidad_exonerada"),
                DistribuidaCantidad = ObtenerInt(fila, "distribuida_cantidad", "cantidad_distribuida")
            };
        }

        public DataTable DeudasPorEstado(string estado)
        {
            return reporteBC.DeudasPorEstado(estado);
        }

        public DataTable DeudasPorEstadoEntreFechas(string estado, DateTime fechaInicio, DateTime fechaFin)
        {
            return reporteBC.DeudasPorEstadoEntreFechas(estado, fechaInicio, fechaFin);
        }

        private static decimal ObtenerDecimal(DataRow fila, string primary, string? secondary = null, string? tertiary = null, decimal defaultValue = 0m)
        {
            var valor = ObtenerValor(fila, primary, secondary, tertiary);
            return valor == null || valor == DBNull.Value ? defaultValue : Convert.ToDecimal(valor);
        }

        private static int ObtenerInt(DataRow fila, string primary, string? secondary = null, string? tertiary = null, int defaultValue = 0)
        {
            var valor = ObtenerValor(fila, primary, secondary, tertiary);
            return valor == null || valor == DBNull.Value ? defaultValue : Convert.ToInt32(valor);
        }

        private static object? ObtenerValor(DataRow fila, string primary, string? secondary = null, string? tertiary = null)
        {
            if (fila.Table.Columns.Contains(primary))
                return fila[primary];

            if (!string.IsNullOrWhiteSpace(secondary) && fila.Table.Columns.Contains(secondary))
                return fila[secondary];

            if (!string.IsNullOrWhiteSpace(tertiary) && fila.Table.Columns.Contains(tertiary))
                return fila[tertiary];

            return null;
        }

        private static ReporteResponse? MapearResumenIngresos(DataTable tabla)
        {
            if (tabla.Rows.Count == 0)
                return null;

            var fila = tabla.Rows[0];

            return new ReporteResponse
            {
                TotalIngresos = ObtenerDecimal(fila, "total_ingresos"),
                CantidadPagos = ObtenerInt(fila, "cantidad_pagos")
            };
        }
    }
}
