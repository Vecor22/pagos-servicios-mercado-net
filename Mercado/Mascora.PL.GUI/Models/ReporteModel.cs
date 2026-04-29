using Mercado.BL.BC;
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

        public DataTable ResumenIngresosMes()
        {
            return reporteBC.ResumenIngresosMes();
        }

        public DataTable ResumenIngresosAnio()
        {
            return reporteBC.ResumenIngresosAnio();
        }

        public DataTable ResumenIngresosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return reporteBC.ResumenIngresosEntreFechas(fechaInicio, fechaFin);
        }

        public DataTable ResumenDeudas()
        {
            return reporteBC.ResumenDeudas();
        }

        public DataTable DeudasPorEstado(string estado)
        {
            return reporteBC.DeudasPorEstado(estado);
        }

        public DataTable DeudasPorEstadoEntreFechas(string estado, DateTime fechaInicio, DateTime fechaFin)
        {
            return reporteBC.DeudasPorEstadoEntreFechas(estado, fechaInicio, fechaFin);
        }
    }
}
