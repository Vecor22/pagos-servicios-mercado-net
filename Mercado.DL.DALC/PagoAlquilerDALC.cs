using Microsoft.Data.SqlClient;
using Mercado.BL.BE;
using System.Data;

namespace Mercado.DL.DALC
{
    public class PagoAlquilerDALC
    {
        public List<PagoAlquilerBE> listar()
        {
            var lista = new List<PagoAlquilerBE>();
            try
            {
                using var con = Conexion.getConexion();
                using var cmd = new SqlCommand("sp_ListarPagoAlquiler", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                    lista.Add(Mapear(dr));
            }
            catch { }
            return lista;
        }

        public PagoAlquilerBE? buscar(int idPago)
        {
            try
            {
                using var con = Conexion.getConexion();
                using var cmd = new SqlCommand("sp_BuscarPagoAlquiler", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPago", idPago);
                using var dr = cmd.ExecuteReader();
                if (dr.Read()) return Mapear(dr);
            }
            catch { }
            return null;
        }

        public int insertar(PagoAlquilerBE p)
        {
            using var con = Conexion.getConexion();
            using var cmd = new SqlCommand("sp_GuardarPagoAlquiler", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPuesto",  p.IdPuesto);
            cmd.Parameters.AddWithValue("@fechaPago", p.FechaPago);
            cmd.Parameters.AddWithValue("@monto",     p.Monto);
            cmd.Parameters.AddWithValue("@mes",       p.Mes);
            cmd.Parameters.AddWithValue("@anio",      p.Anio);
            cmd.Parameters.AddWithValue("@estado",    p.Estado);
            var param = new SqlParameter("@nuevoID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(param.Value);
        }

        public bool actualizar(PagoAlquilerBE p)
        {
            using var con = Conexion.getConexion();
            using var cmd = new SqlCommand("sp_ActualizarPagoAlquiler", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPago",    p.IdPago);
            cmd.Parameters.AddWithValue("@idPuesto",  p.IdPuesto);
            cmd.Parameters.AddWithValue("@fechaPago", p.FechaPago);
            cmd.Parameters.AddWithValue("@monto",     p.Monto);
            cmd.Parameters.AddWithValue("@mes",       p.Mes);
            cmd.Parameters.AddWithValue("@anio",      p.Anio);
            cmd.Parameters.AddWithValue("@estado",    p.Estado);
            var param = new SqlParameter("@procesado", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return Convert.ToBoolean(param.Value);
        }

        public bool eliminar(int idPago)
        {
            using var con = Conexion.getConexion();
            using var cmd = new SqlCommand("sp_EliminarPagoAlquiler", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPago", idPago);
            var param = new SqlParameter("@procesado", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return Convert.ToBoolean(param.Value);
        }

        public List<PagoAlquilerBE> listarPorAnio(int anio)
        {
            var lista = new List<PagoAlquilerBE>();
            try
            {
                using var con = Conexion.getConexion();
                using var cmd = new SqlCommand("sp_ReportePagosPorAnio", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@anio", anio);
                using var dr = cmd.ExecuteReader();
                while (dr.Read()) lista.Add(Mapear(dr));
            }
            catch { }
            return lista;
        }

        private static PagoAlquilerBE Mapear(SqlDataReader dr) => new()
        {
            IdPago       = dr.GetInt32(0),
            IdPuesto     = dr.GetInt32(1),
            CodigoPuesto = dr.IsDBNull(2) ? "" : dr.GetString(2),
            NombreSocio  = dr.IsDBNull(3) ? "" : dr.GetString(3),
            FechaPago    = dr.GetDateTime(4),
            Monto        = dr.GetDecimal(5),
            Mes          = dr.IsDBNull(6) ? "" : dr.GetString(6),
            Anio         = dr.GetInt32(7),
            Estado       = dr.IsDBNull(8) ? "" : dr.GetString(8)
        };
    }
}
