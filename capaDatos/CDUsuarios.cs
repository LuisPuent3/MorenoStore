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
    public class CDUsuarios
    {
        private readonly CDConexion con = new CDConexion();
        private readonly CEUsuarios ce = new CEUsuarios();

        //CRUD USUARIOS 

        #region INSERTAR 
        public void CDinsertar(CEUsuarios Usuarios)
        {
            MySqlCommand com = new MySqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "U_Insertar",
                CommandType = CommandType.StoredProcedure,

            };
            
            com.Parameters.AddWithValue("nombres", Usuarios.Nombres);
            com.Parameters.AddWithValue("apellidoP", Usuarios.ApellidoP);
            com.Parameters.AddWithValue("apellidoM", Usuarios.ApellidoM);
            com.Parameters.AddWithValue("dni", Usuarios.Dni);
            com.Parameters.AddWithValue("email", Usuarios.Email);
            com.Parameters.AddWithValue("telefono", Usuarios.Telefono);
            com.Parameters.AddWithValue("idPrivilegio", Usuarios.IdPrivilegio);
            com.Parameters.AddWithValue("img", Usuarios.Img);
            com.Parameters.AddWithValue("usuario", Usuarios.Usuario);
            com.Parameters.AddWithValue("contrasenia", Usuarios.Contrasenia);
            com.Parameters.AddWithValue("patron", Usuarios.Patron);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
            #endregion

            #region CONSULTA

            public CEUsuarios CDConsulta( int idUsuario)
            {
                MySqlDataAdapter da = new MySqlDataAdapter("U_Consultar", con.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@idUsuarios", MySqlDbType.Int64).Value = idUsuario;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt;
                dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                ce.Nombres = Convert.ToString(row[1]);
                ce.ApellidoP = Convert.ToString(row[2]);
                ce.ApellidoM = Convert.ToString(row[3]);
                ce.Dni = Convert.ToInt32(row[4]);
                ce.Email = Convert.ToString(row[5]);
                ce.Telefono = Convert.ToInt32(row[6]);
                ce.IdPrivilegio = Convert.ToInt32(row[7]);
                ce.Img = (Byte[])row[8];
                ce.Usuario = Convert.ToString(row[9]);
                return ce;


            }
        #endregion

        #region ELIMINAR
        public void CDEliminar(CEUsuarios Usuarios)
        {
            MySqlCommand com = new MySqlCommand();  
            com.Connection=con.AbrirConexion();
            com.CommandText = "U_Eliminar";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idUsuario", Usuarios.IdUsuarios);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion
        #region ACTUALIZAR DATOS

        public void CDActualizarDatos (CEUsuarios Usuarios)
        {
            MySqlCommand com = new MySqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "U_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("nombres", Usuarios.Nombres);
            com.Parameters.AddWithValue("apellidoP", Usuarios.ApellidoP);
            com.Parameters.AddWithValue("apellidoM", Usuarios.ApellidoM);
            com.Parameters.AddWithValue("dni", Usuarios.Dni);
            com.Parameters.AddWithValue("email", Usuarios.Email);
            com.Parameters.AddWithValue("telefono", Usuarios.Telefono);
            com.Parameters.AddWithValue("idPrivilegio", Usuarios.IdPrivilegio);
            com.Parameters.AddWithValue("usuario", Usuarios.Usuario);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region ACTUALIZAR PASS

        public void CDActualizarPass(CEUsuarios Usuarios)
        {
            MySqlCommand com = new MySqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "U_ActualizarPass",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("idUsuarios", Usuarios.IdUsuarios);
            com.Parameters.AddWithValue("contrasenia", Usuarios.Contrasenia);
            com.Parameters.AddWithValue("patron", Usuarios.Patron);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion
        #region ACTUALIZAR IMG

        public void CDActualizarIMG(CEUsuarios Usuarios)
        {
            MySqlCommand com = new MySqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "U_ActualizarIMG",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("idUsuarios", Usuarios.IdUsuarios);
            com.Parameters.AddWithValue("img", Usuarios.Img);
            con.CerrarConexion();
        }

        #endregion


    }
}

