using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace capaDatos
{
    public class CDConexion
    {
        private readonly MySqlConnection con = new MySqlConnection("Server=localhost;Uid=root;Database=store;Port=3306;");
        public MySqlConnection AbrirConexion()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                return con;
            }
        }
        public MySqlConnection CerrarConexion()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                return con;
            }
        }
    }
}