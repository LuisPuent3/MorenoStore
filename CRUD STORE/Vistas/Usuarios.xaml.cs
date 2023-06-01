using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

namespace CRUD_STORE.Vistas
{
    /// <summary>
    /// Interaction logic for Usuarios.xaml
    /// </summary>
    public partial class Usuarios : UserControl
    {
        public Usuarios()
        {
            InitializeComponent();
            CargarDatos();


        }

        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString);


        void CargarDatos()
        {
            con.Open();
            string query = "SELECT idUsuarios, nombres, apellidoP, apellidoM, email, telefono, nombrePrivilegio FROM Usuarios us inner join Privilegios pr on us.idPrivilegio = pr.idPrivilegio order by idUsuarios ASC";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter data = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            GridDatos.ItemsSource = dt.DefaultView;
            con.Close();

        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            CRUDUsuarios ventana = new CRUDUsuarios();
            FrameUsuarios.Content = ventana;
            Contenido.Visibility = Visibility.Hidden;
            ventana.btnCrear.Visibility = Visibility.Visible;
        }

        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.idUsuario = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
            ventana.titulo.Text = "Consulta de Usuario";
            ventana.tbNombres.IsEnabled = false;
            ventana.tbApellidoM.IsEnabled = false;
            ventana.tbApellidoP.IsEnabled = false;
            ventana.tbTelefono.IsEnabled = false;
            ventana.tbEmail.IsEnabled = false;
            ventana.tbDNI.IsEnabled = false;
            // ventana.tbFecha.IsEnabled = false;
            ventana.cbPrivilegio.IsEnabled = false;
            ventana.tbUsuario.IsEnabled = false;
            ventana.tbContrasenia.IsEnabled = false;
            ventana.btnSubir.IsEnabled = false;
            Contenido.Visibility = Visibility.Hidden;

        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.idUsuario = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
            ventana.titulo.Text = "Actualizar Usuario";
            ventana.tbNombres.IsEnabled = true;
            ventana.tbApellidoM.IsEnabled = true;
            ventana.tbApellidoP.IsEnabled = true;
            ventana.tbTelefono.IsEnabled = true;
            ventana.tbEmail.IsEnabled = true;
            ventana.tbDNI.IsEnabled = true;
            //ventana.tbFecha.IsEnabled = true;
            ventana.cbPrivilegio.IsEnabled = true;
            ventana.tbUsuario.IsEnabled = true;
            ventana.tbContrasenia.IsEnabled = true;
            ventana.btnSubir.IsEnabled = true;
            ventana.btnActualizar.Visibility = Visibility.Visible;
            Contenido.Visibility = Visibility.Hidden;
        }
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDUsuarios ventana = new CRUDUsuarios();
            ventana.idUsuario = id;
            ventana.Consultar();
            FrameUsuarios.Content = ventana;
            ventana.titulo.Text = "Eliminar Usuario";
            ventana.tbNombres.IsEnabled = false;
            ventana.tbApellidoM.IsEnabled = false;
            ventana.tbApellidoP.IsEnabled = false;
            ventana.tbTelefono.IsEnabled = false;
            ventana.tbEmail.IsEnabled = false;
            ventana.tbDNI.IsEnabled = false;
            // ventana.tbFecha.IsEnabled = false;
            ventana.cbPrivilegio.IsEnabled = false;
            ventana.tbUsuario.IsEnabled = false;
            ventana.tbContrasenia.IsEnabled = false;
            ventana.btnSubir.IsEnabled = false;
            ventana.btnEliminar.Visibility = Visibility.Visible;
            Contenido.Visibility = Visibility.Hidden;

        }
        #region Buscando
        private void Buscando(object sender, TextChangedEventArgs e)
        {
            string buscar = TextBoxBuscar.Text;
            {
                con.Open();
                string queryBuscar = "SELECT Usuarios.*, Privilegios.nombrePrivilegio " +
                                     "FROM Usuarios " +
                                     "INNER JOIN Privilegios ON Usuarios.idPrivilegio = Privilegios.idPrivilegio " +
                                     "WHERE nombres LIKE @buscar OR apellidoP LIKE @buscar OR apellidoM LIKE @buscar";
                MySqlCommand cmd = new MySqlCommand(queryBuscar, con);
                cmd.Parameters.AddWithValue("@buscar", buscar + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                GridDatos.ItemsSource = dataTable.DefaultView;
                con.Close();    
            }

        }
        #endregion

    }
}