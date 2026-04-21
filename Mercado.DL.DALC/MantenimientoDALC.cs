using Microsoft.Data.SqlClient;
using Mercado.BL.BE;
using System.Data;

namespace Mercado.DL.DALC
{
    public class MantenimientoDALC
    {
        public List<MantenimientoBE> listar()
        {
            var lista = new List<MantenimientoBE>();
            try
            {
                using var con = Conexion.getConexion();
                using var cmd = new SqlCommand("sp_ListarMantenimiento", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using var dr = cmd.ExecuteReader();
                while (dr.Read()) lista.Add(Mapear(dr));
            }
            catch { }
            return lista;
        }

        public MantenimientoBE? buscar(int id)
        {
            try
            {
                using var con = Conexion.getConexion();
                using var cmd = new SqlCommand("sp_BuscarMantenimiento", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMantenimiento", id);
                using var dr = cmd.ExecuteReader();
                if (dr.Read()) return Mapear(dr);
            }
            catch { }
            return null;
        }

        public int insertar(MantenimientoBE m)
        {
            using var con = Conexion.getConexion();
            using var cmd = new SqlCommand("sp_GuardarMantenimiento", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tipo",        m.Tipo);
            cmd.Parameters.AddWithValue("@descripcion", m.Descripcion);
            cmd.Parameters.AddWithValue("@fecha",       m.Fecha);
            cmd.Parameters.AddWithValue("@monto",       m.Monto);
            cmd.Parameters.AddWithValue("@proveedor",   m.Proveedor ?? "");
            cmd.Parameters.AddWithValue("@mes",         m.Mes);
            cmd.Parameters.AddWithValue("@anio",        m.Anio);
            var param = new SqlParameter("@nuevoID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(param.Value);
        }

        public bool actualizar(MantenimientoBE m)
        {
            using var con = Conexion.getConexion();
            using var cmd = new SqlCommand("sp_ActualizarMantenimiento", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idMantenimiento", m.IdMantenimiento);
            cmd.Parameters.AddWithValue("@tipo",            m.Tipo);
            cmd.Parameters.AddWithValue("@descripcion",     m.Descripcion);
            cmd.Parameters.AddWithValue("@fecha",           m.Fecha);
            cmd.Parameters.AddWithValue("@monto",           m.Monto);
            cmd.Parameters.AddWithValue("@proveedor",       m.Proveedor ?? "");
            cmd.Parameters.AddWithValue("@mes",             m.Mes);
            cmd.Parameters.AddWithValue("@anio",            m.Anio);
            var param = new SqlParameter("@procesado", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return Convert.ToBoolean(param.Value);
        }

        public bool eliminar(int id)
        {
            using var con = Conexion.getConexion();
            using var cmd = new SqlCommand("sp_EliminarMantenimiento", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idMantenimiento", id);
            var param = new SqlParameter("@procesado", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return Convert.ToBoolean(param.Value);
        }

        public List<MantenimientoBE> listarPorAnio(int anio)
        {
            var lista = new List<MantenimientoBE>();
            try
            {
                using var con = Conexion.getConexion();
                using var cmd = new SqlCommand("sp_ReporteMantenimientoPorAnio", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@anio", anio);
                using var dr = cmd.ExecuteReader();
                while (dr.Read()) lista.Add(Mapear(dr));
            }
            catch { }
            return lista;
        }

        private static MantenimientoBE Mapear(SqlDataReader dr) => new()
        {
            IdMantenimiento = dr.GetInt32(0),
            Tipo            = dr.IsDBNull(1) ? "" : dr.GetString(1),
            Descripcion     = dr.IsDBNull(2) ? "" : dr.GetString(2),
            Fecha           = dr.GetDateTime(3),
            Monto           = dr.GetDecimal(4),
            Proveedor       = dr.IsDBNull(5) ? "" : dr.GetString(5),
            Mes             = dr.IsDBNull(6) ? "" : dr.GetString(6),
            Anio            = dr.GetInt32(7)
        };
    }
}
