using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.DL.DALC
{
    public class Conexion
    {
        private static string cadena = 
            "Server=.;Database=MercadoDB;User Id=sa;" +
            "Password=sql;TrustServerCertificate=True;";

        public static SqlConnection getConnection()
        {
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
    }
}
