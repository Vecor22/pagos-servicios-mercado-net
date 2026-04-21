using Microsoft.Data.SqlClient;

namespace Mercado.DL.DALC
{
    public class Conexion
    {
        private static string cadena = "Server=.;Database=Mercado261;User Id=sa;Password=tatiana;TrustServerCertificate=True;";

        public static SqlConnection getConexion()
        {
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
    }
}
