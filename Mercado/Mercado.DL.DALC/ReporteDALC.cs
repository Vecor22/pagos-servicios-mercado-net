using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.DL.DALC
{
    public class ReporteDALC
    {
        private const string SpResumenIngresosDia = "usp_Reporte_ResumenIngresosDia";
        private const string SpResumenIngresosMes = "usp_Reporte_ResumenIngresosMes";
        private const string SpResumenIngresosAnio = "usp_Reporte_ResumenIngresosAnio";
        private const string SpResumenIngresosEntreFechas = "usp_Reporte_ResumenIngresosEntreFechas";
        private const string SpResumenDeudas = "usp_Reporte_ResumenDeudas";
        private const string SpResumenDeudasEntreFechas = "usp_Reporte_ResumenDeudasEntreFechas";
        private const string SpDeudasPorEstado = "usp_Reporte_DeudasPorEstado";
        private const string SpDeudasPorEstadoEntreFechas = "usp_Reporte_DeudasPorEstadoEntreFechas";

        public DataTable ResumenIngresosDia()
        {
            return EjecutarConsulta(SpResumenIngresosDia);
        }

        public DataTable ResumenIngresosMes()
        {
            return EjecutarConsulta(SpResumenIngresosMes);
        }

        public DataTable ResumenIngresosAnio()
        {
            return EjecutarConsulta(SpResumenIngresosAnio);
        }

        public DataTable ResumenIngresosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@fecha_inicio", fechaInicio.Date),
                new SqlParameter("@fecha_fin", fechaFin.Date)
            };

            return EjecutarConsulta(SpResumenIngresosEntreFechas, parametros);
        }

        public DataTable ResumenDeudas()
        {
            return EjecutarConsulta(SpResumenDeudas);
        }

        public DataTable DeudasPorEstado(string estado)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@estado", estado)
            };

            return EjecutarConsulta(SpDeudasPorEstado, parametros);
        }

        public DataTable DeudasPorEstadoEntreFechas(string estado, DateTime fechaInicio, DateTime fechaFin)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@estado", estado),
                new SqlParameter("@fecha_inicio", fechaInicio.Date),
                new SqlParameter("@fecha_fin", fechaFin.Date)
            };

            return EjecutarConsulta(SpDeudasPorEstadoEntreFechas, parametros);
        }

        public DataTable ResumenDeudasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@fecha_inicio", fechaInicio.Date),
                new SqlParameter("@fecha_fin", fechaFin.Date)
            };

            return EjecutarConsulta(SpResumenDeudasEntreFechas, parametros);
        }

        private static DataTable EjecutarConsulta(string nombreProcedimiento, SqlParameter[]? parametros = null)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(tabla);
                }
            }

            return tabla;
        }
    }
}
