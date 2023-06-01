using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using capaEntidad;
using System.Data;

namespace capaDatos
{
    public class CDPrivilegios
    {
        readonly CDConexion con = new CDConexion();
        readonly CEPrivilegios ce = new CEPrivilegios();

        #region PRIVILEGIO
        public int idPrivilegio(string nombrePrivilegio)
        {
            MySqlCommand com = new MySqlCommand() { 
            Connection = con.AbrirConexion(),
            CommandText = "P_idPrivilegio",
            CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("nombrePrivilegio", nombrePrivilegio);
            object valor = com.ExecuteScalar();
            int idPrivilegio     = (int)valor;
            con.CerrarConexion();
            return idPrivilegio;
        }
        #endregion

        #region NOMBREPRIVILEGIO
        public CEPrivilegios nombrePrivilegio(string nombrePrivilegio) {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("P_NombrePrivilegio",con.AbrirConexion());
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dataAdapter.SelectCommand.Parameters.Add("idPrivilegio", MySqlDbType.Int32).Value = idPrivilegio;
            DataSet ds = new DataSet();
            ds.Clear();
            dataAdapter.Fill(ds);
            DataTable dt;
            dt=ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.NombrePrivilegio = Convert.ToString(row[0]);

            return ce;
        }
        #endregion

    }
}
