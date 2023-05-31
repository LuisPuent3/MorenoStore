using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using capaEntidad;

namespace CRUD_STORE.Vistas
{
    /// <summary>
    /// Interaction logic for CRUDUsuarios.xaml
    /// </summary>
    public partial class CRUDUsuarios : Page
    {
        public CRUDUsuarios()
        {
            InitializeComponent();
            CargarCB();
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }

        readonly MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString);

        void CargarCB() { 
            con.Open();
            string query = " SELECT nombrePrivilegio from Privilegios";
            MySqlCommand cmd = new MySqlCommand(query,con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbPrivilegio.Items.Add(dr["nombrePrivilegio"].ToString());
            }
            con.Close();
        }
        #region CRUD
        #region CREATE

        private void Crear(object sender, RoutedEventArgs e)
        {
            if (tbNombres.Text== "" || tbApellidoP.Text=="" || tbApellidoM.Text == "" || tbDNI.Text == "" || tbEmail.Text == "" || tbTelefono.Text == ""  || cbPrivilegio.Text == "" || tbUsuario.Text == "" || tbContrasenia.Text == "")
            {
                MessageBox.Show("Faltan campos por llenar");
            }
            else
            {
                con.Open();
                string query2 = "SELECT idPrivilegio FROM Privilegios WHERE nombrePrivilegio = @nombrePrivilegio";
                MySqlCommand cmd2 = new MySqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@nombrePrivilegio", cbPrivilegio.Text);
                object valor = cmd2.ExecuteScalar();
                int idPrivilegio = (int)valor;
                string patron = "admin1234";
                if (imagenSubida == true)
                {
                    string query3 = "INSERT INTO Usuarios (nombres, apellidoP, apellidoM, dni, email, telefono, idPrivilegio, img, usuario, contrasenia) VALUES (@nombres, @apellidoP, @apellidoM, @dni, @email, @telefono, @idPrivilegio, @img, @usuario, AES_ENCRYPT(@contrasenia, @patron))";
                    MySqlCommand cm3 = new MySqlCommand(query3, con);
                    cm3.Parameters.AddWithValue("@nombres", tbNombres.Text);
                    cm3.Parameters.AddWithValue("@apellidoP", tbApellidoP.Text);
                    cm3.Parameters.AddWithValue("@apellidoM", tbApellidoM.Text);
                    cm3.Parameters.AddWithValue("@dni", tbDNI.Text);
                    cm3.Parameters.AddWithValue("@email", tbEmail.Text);
                    cm3.Parameters.AddWithValue("@telefono", tbTelefono.Text);
                    cm3.Parameters.AddWithValue("@idPrivilegio", idPrivilegio);
                    cm3.Parameters.AddWithValue("@img", data);
                    cm3.Parameters.AddWithValue("@usuario", tbUsuario.Text);
                    cm3.Parameters.AddWithValue("@contrasenia", tbContrasenia.Text);
                    cm3.Parameters.AddWithValue("@patron", patron);
                    cm3.ExecuteNonQuery();
                    Content = new Usuarios();
                }
                else
                {
                    MessageBox.Show("Falta agregar una fotografía para el usuario.");
                }
                con.Close();


            }
        }

        #endregion

        public int idUsuario;
        #region CONSULTAR
        public void Consultar()
        {
            con.Open();
            MySqlCommand cmd4 = new MySqlCommand("SELECT * FROM Usuarios INNER JOIN Privilegios ON Usuarios.idPrivilegio = Privilegios.idPrivilegio WHERE idUsuarios = '" + idUsuario + "'", con);
            MySqlDataReader reader4 = cmd4.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            reader4.Read();
            this.tbNombres.Text = reader4["nombres"].ToString();
            this.tbApellidoM.Text = reader4["apellidoM"].ToString();
            this.tbApellidoP.Text = reader4["apellidoP"].ToString();
            this.tbDNI.Text = reader4["dni"].ToString();
            this.tbEmail.Text = reader4["email"].ToString();
            this.tbTelefono.Text = reader4["telefono"].ToString();
            //this.tbFecha.Text = reader4["fechaNacimiento"].ToString();
            this.cbPrivilegio.Text = reader4["nombrePrivilegio"].ToString();
            this.tbUsuario.Text = reader4["usuario"].ToString();
            // this.tbContrasenia.Text = "Contraseña encriptada";
            reader4.Close();
            // obtengo imagen

            DataSet dataSet = new DataSet();
            string query5 = "SELECT img FROM Usuarios WHERE idUsuarios = '" + idUsuario + "'";
            MySqlDataAdapter sqldata = new MySqlDataAdapter(query5, con);
            sqldata.Fill(dataSet);
            byte[] data = (byte[])dataSet.Tables[0].Rows[0][0];
            MemoryStream ms = new MemoryStream(data);
            ms.Position = 0;

            System.Drawing.Image img = null;
            BitmapImage bi = new BitmapImage();

            // Intenta abrir la imagen en diferentes formatos
            try
            {
                img = System.Drawing.Image.FromStream(ms);
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
            }
            catch (ArgumentException)
            {
                // Si falla con el formato BMP, intenta con otros formatos
                ms.Position = 0;
                img = null;

                // Intenta abrir la imagen como PNG
                try
                {
                    img = System.Drawing.Image.FromStream(ms);
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                }
                catch (ArgumentException)
                {
                    // Si falla con el formato PNG, intenta con formato JPEG
                    ms.Position = 0;
                    img = null;

                    // Intenta abrir la imagen como JPEG
                    try
                    {
                        img = System.Drawing.Image.FromStream(ms);
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();
                    }
                    catch (ArgumentException)
                    {
                        // Si sigue fallando, muestra un mensaje de error o realiza alguna acción adicional
                        MessageBox.Show("No se pudo abrir la imagen en ninguno de los formatos compatibles.");
                    }
                }
            }

            imagen.Source = bi;

            // conexion cerrada

            con.Close();

        }
        #endregion

        #region DELETE
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            con.Open();
            string queryDelete = "DELETE FROM Usuarios WHERE idUsuarios = @idUsuario";
            MySqlCommand cmdDelete = new MySqlCommand(queryDelete, con);
            cmdDelete.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmdDelete.ExecuteNonQuery();

            con.Close();
            Content = new Usuarios(); 

        }
        #endregion
        #region UPDATE 
        private void Actualizar(object sender, RoutedEventArgs e)
        {
           con.Open();
            string queryUpdate = " select idPrivilegio from Privilegios where nombrePrivilegio ='" + cbPrivilegio.Text + "'";
            MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, con);
            object valor = cmdUpdate.ExecuteScalar();
            int idPrivilegio = (int)valor;
            string patron = "admin1234";
            if (tbNombres.Text == "" || tbApellidoP.Text == "" || tbApellidoM.Text == "" || tbDNI.Text == "" || tbEmail.Text == "" || tbTelefono.Text == "" || cbPrivilegio.Text == "" || tbUsuario.Text == "")
            {
                MessageBox.Show("Faltan campos por llenar");
            }
            else
            {
                string queryUpdate2 = "UPDATE Usuarios SET nombres = '" + tbNombres.Text + "', apellidoP = '" + tbApellidoP.Text + "', apellidoM = '" + tbApellidoM.Text + "', dni = '" + int.Parse(tbDNI.Text) + "', email = '" + tbEmail.Text + "', telefono = '" + int.Parse(tbTelefono.Text) + "', idPrivilegio = '" + idPrivilegio + "', usuario = '" + tbUsuario.Text + " ' where idUsuarios = '"+idUsuario+"' ";
                MySqlCommand cmdUpdate2 = new MySqlCommand(queryUpdate2, con);

                //validacion de imagen 

                if (imagenSubida == true)
                {
                    string queryimg = "UPDATE Usuarios SET img = @img WHERE idUsuarios = @idUsuario";
                    MySqlCommand cmdimg = new MySqlCommand(queryimg, con);
                    cmdimg.Parameters.Add("@img", MySqlDbType.VarBinary).Value = data;
                    cmdimg.Parameters.Add("@idUsuario", MySqlDbType.Int32).Value = idUsuario;
                    cmdimg.ExecuteNonQuery();


                }
                cmdUpdate2.ExecuteNonQuery();

            }
            if (tbContrasenia.Text != "")
            {

                MySqlCommand cmdPassEncrypt = new MySqlCommand("UPDATE Usuarios SET contrasenia = AES_ENCRYPT(@nuevaContrasenia, '" + patron + "') WHERE idUsuarios = @idUsuario", con);
                cmdPassEncrypt.Parameters.AddWithValue("@nuevaContrasenia", tbContrasenia.Text);
                cmdPassEncrypt.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmdPassEncrypt.ExecuteNonQuery();
            }
            
            con.Close();
            Content = new Usuarios();

        }


        #endregion
        #endregion

        byte[] data;

        #region Imagen
        private bool imagenSubida;
        private void Subir(object sender, RoutedEventArgs e)
        {
           OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                FileStream fs = new FileStream(ofd.FileName,FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                imagen.SetValue(Image.SourceProperty, imgs.ConvertFromString(ofd.FileName.ToString()));


            }
            imagenSubida = true;
        }
        #endregion


    }
}
