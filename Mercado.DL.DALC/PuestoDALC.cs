using Microsoft.Data.SqlClient;
using Mercado.BL.BE;
using System.Data;

namespace Mercado.DL.DALC
{
    public class PuestoDALC
    {
        public List<PuestoBE> listar()
        {
            List<PuestoBE> lista = new List<PuestoBE>();
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarPuesto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        PuestoBE puesto = new PuestoBE();
                        puesto.IdPuesto    = dr.GetInt32(0);
                        puesto.Codigo      = dr.GetString(1);
                        puesto.Sector      = dr.IsDBNull(2) ? "" : dr.GetString(2);
                        puesto.NombreSocio = dr.GetString(3);
                        puesto.Socio       = new SocioBE { IdSocio = dr.IsDBNull(4) ? 0 : dr.GetInt32(4) };
                        lista.Add(puesto);
                    }
                }
            }
            catch (Exception) { }
            return lista;
        }

        public PuestoBE buscar(int idPuesto)
        {
            PuestoBE puesto = null;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_BuscarPuesto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idPuesto", idPuesto);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        puesto = new PuestoBE();
                        puesto.IdPuesto    = dr.GetInt32(0);
                        puesto.Codigo      = dr.GetString(1);
                        puesto.Sector      = dr.IsDBNull(2) ? "" : dr.GetString(2);
                        puesto.Socio       = new SocioBE { IdSocio = dr.IsDBNull(3) ? 0 : dr.GetInt32(3) };
                        puesto.NombreSocio = dr.GetString(4);
                    }
                }
            }
            catch (Exception) { }
            return puesto;
        }

        public int insertar(PuestoBE puestoBE)
        {
            int idInsertado = 0;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_GuardarPuesto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo",  puestoBE.Codigo);
                    cmd.Parameters.AddWithValue("@sector",  puestoBE.Sector ?? "");
                    cmd.Parameters.AddWithValue("@idSocio", puestoBE.Socio?.IdSocio > 0 ? puestoBE.Socio.IdSocio : DBNull.Value);

                    SqlParameter param = new SqlParameter("@nuevoID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    idInsertado = Convert.ToInt32(param.Value);
                }
            }
            catch (Exception) { throw; }
            return idInsertado;
        }

        public bool actualizar(PuestoBE puestoBE)
        {
            bool procesado = false;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarPuesto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idPuesto", puestoBE.IdPuesto);
                    cmd.Parameters.AddWithValue("@codigo",   puestoBE.Codigo);
                    cmd.Parameters.AddWithValue("@sector",   puestoBE.Sector ?? "");
                    cmd.Parameters.AddWithValue("@idSocio",  puestoBE.Socio?.IdSocio > 0 ? puestoBE.Socio.IdSocio : DBNull.Value);

                    SqlParameter param = new SqlParameter("@procesado", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    procesado = Convert.ToBoolean(param.Value);
                }
            }
            catch (Exception) { throw; }
            return procesado;
        }

        public bool eliminar(int idPuesto)
        {
            bool procesado = false;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarPuesto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idPuesto", idPuesto);

                    SqlParameter param = new SqlParameter("@procesado", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    procesado = Convert.ToBoolean(param.Value);
                }
            }
            catch (Exception) { throw; }
            return procesado;
        }
    }
}
