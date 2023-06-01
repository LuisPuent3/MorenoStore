using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace CRUD_STORE.Vistas
{
    /// <summary>
    /// Interaction logic for CRUDProductos.xaml
    /// </summary>
    public partial class CRUDProductos : Page
    {
        public CRUDProductos()
        {
            InitializeComponent();
        }

        private void AgregarProductos(object sender, EventArgs e)
        {
            Content = new Productos();
        }

        private void RegresarProductos(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }

        readonly MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString);

        void CargarGrupo()
        {
            con.Open();
            string query = " SELECT nombreGrupo from Grupos";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbGrupo.Items.Add(dr["nombreGrupo"].ToString());
            }
            con.Close();
        }
        #region CRUD
        #region CREATE

        private void Crear(object sender, RoutedEventArgs e)
        {
            if (tbCodigo.Text == "" || tbNombreProducto.Text == "" || tbPrecio.Text == "" || tbCantidad.Text == "" || tbMedida.Text == "" || cbGrupo.Text == "" || tbDescripcion.Text == "")
            {
                MessageBox.Show("Faltan campos por llenar");
            }
             
            else
            {
                con.Open();
                string query2 = "SELECT idGrupo FROM Grupos WHERE nombreGrupo = @nombreGrupo";
                MySqlCommand cmd2 = new MySqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@nombreGrupo", cbGrupo.Text);
                object valor = cmd2.ExecuteScalar();
                int idGrupo= (int)valor;
                if (imagenSubida == true)
                {
                    string query3 = "INSERT INTO Articulos (codigo, nombre, cantidad, precio, descripcion, idGrupo, medida, img) VALUES (@codigo, @nombre, @cantidad, @precio, @descripcion, @idGrupo, @medida, @img";
                    MySqlCommand cm3 = new MySqlCommand(query3, con);
                    cm3.Parameters.AddWithValue("@codigo", tbCodigo.Text);
                    cm3.Parameters.AddWithValue("@nombre", tbNombreProducto.Text);
                    cm3.Parameters.AddWithValue("@cantidad", tbCantidad.Text);
                    cm3.Parameters.AddWithValue("@precio", tbPrecio.Text);
                    cm3.Parameters.AddWithValue("@descripcion", tbDescripcion.Text);
                    cm3.Parameters.AddWithValue("@idGrupo", idGrupo);
                    cm3.Parameters.AddWithValue("@medida", tbMedida.Text);
                    cm3.Parameters.AddWithValue("@img", data);
                    cm3.ExecuteNonQuery();
                    Content = new Productos();
                }
                else
                {
                    MessageBox.Show("Falta agregar una fotografía para el usuario.");
                }
                con.Close();


            }
        }

        #endregion

        public int idArticulo;

        #region CONSULTAR
        public void Consultar()
        {
            con.Open();
            MySqlCommand cmd4 = new MySqlCommand("SELECT * FROM Articulos INNER JOIN Grupos ON Articulos.idGrupo = Grupos.idGrupo WHERE idArticulos = '" + idArticulo + "'", con);
            MySqlDataReader reader4 = cmd4.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            reader4.Read();
            this.tbCodigo.Text = reader4["codigo"].ToString();
            this.tbNombreProducto.Text = reader4["nombre"].ToString();
            this.tbCantidad.Text = reader4["cantidad"].ToString();
            this.tbPrecio.Text = reader4["precio"].ToString();
            this.tbDescripcion.Text = reader4["descripcion"].ToString();
            this.cbGrupo.Text = reader4["nombreGrupo"].ToString();
            //this.tbFecha.Text = reader4["fechaNacimiento"].ToString();
            this.tbMedida.Text = reader4["medida"].ToString();
            reader4.Close();
            // obtengo imagen

            DataSet dataSet = new DataSet();
            string query5 = "SELECT img FROM Articulo WHERE idArticulo = '" + idArticulo + "'";
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
            string queryDelete = "DELETE FROM Artiuculo WHERE idArticulo = @idArticulo";
            MySqlCommand cmdDelete = new MySqlCommand(queryDelete, con);
            cmdDelete.Parameters.AddWithValue("@idArticulo", idArticulo);
            cmdDelete.ExecuteNonQuery();

            con.Close();
            Content = new Productos();

        }
        #endregion
        #region UPDATE 
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            con.Open();
            string queryUpdate = " select idGrupo from Grupo where nombreGrupo ='" + cbGrupo.Text + "'";
            MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, con);
            object valor = cmdUpdate.ExecuteScalar();
            int idGrupo= (int)valor;
            if (tbCodigo.Text == "" || tbNombreProducto.Text == "" || tbPrecio.Text == "" || tbCantidad.Text == "" || tbMedida.Text == "" || cbGrupo.Text == "" || tbDescripcion.Text == "")
            {
                MessageBox.Show("Faltan campos por llenar");
            }
            else
            {
                string queryUpdate2 = "UPDATE Actualizar SET codigo = '" + tbCodigo.Text + "', nombre = '" + tbNombreProducto.Text + "', cantidad = '" + int.Parse(tbCantidad.Text) + "', precio = '" + float.Parse(tbPrecio.Text) + "', descripcion = '" + tbDescripcion.Text + "', idGrupo = '" + idGrupo + "', medida = '" + tbMedida.Text + "'";
                MySqlCommand cmdUpdate2 = new MySqlCommand(queryUpdate2, con);

                //validacion de imagen 

                if (imagenSubida == true)
                {
                    string queryimg = "UPDATE Articulos SET img = @img WHERE idArticulos = @idArticulos";
                    MySqlCommand cmdimg = new MySqlCommand(queryimg, con);
                    cmdimg.Parameters.Add("@img", MySqlDbType.VarBinary).Value = data;
                    cmdimg.Parameters.Add("@idUsuario", MySqlDbType.Int32).Value = idArticulo;
                    cmdimg.ExecuteNonQuery();


                }
                cmdUpdate2.ExecuteNonQuery();

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
            if (ofd.ShowDialog() == true)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
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

