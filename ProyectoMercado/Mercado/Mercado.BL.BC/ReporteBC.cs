using Mercado.DL.DALC;
using System.Data;

namespace Mercado.BL.BC
{
    public class ReporteBC
    {
        private readonly ReporteDALC reporteDALC = new ReporteDALC();

        public DataTable ResumenIngresosDia()
        {
            return reporteDALC.ResumenIngresosDia();
        }

        public DataTable ResumenIngresosMes()
        {
            return reporteDALC.ResumenIngresosMes();
        }

        public DataTable ResumenIngresosAnio()
        {
            return reporteDALC.ResumenIngresosAnio();
        }

        public DataTable ResumenIngresosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new Exception("La fecha de inicio no puede ser mayor que la fecha fin.");

            return reporteDALC.ResumenIngresosEntreFechas(fechaInicio, fechaFin);
        }

        public DataTable ResumenDeudas()
        {
            return reporteDALC.ResumenDeudas();
        }

        public DataTable DeudasPorEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("El estado es obligatorio.");

            return reporteDALC.DeudasPorEstado(estado);
        }

        public DataTable DeudasPorEstadoEntreFechas(string estado, DateTime fechaInicio, DateTime fechaFin)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("El estado es obligatorio.");

            if (fechaInicio > fechaFin)
                throw new Exception("La fecha de inicio no puede ser mayor que la fecha fin.");

            return reporteDALC.DeudasPorEstadoEntreFechas(estado, fechaInicio, fechaFin);
        }
    }
}
