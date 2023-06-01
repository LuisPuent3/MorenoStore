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
using MySql.Data.MySqlClient;

namespace CRUD_STORE.Vistas
{
    /// <summary>
    /// Interaction logic for Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {
        public Productos()
        {
            InitializeComponent();
        }


        

        #region CRUD




        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString);


        void CargarDatos()
        {
            con.Open();
            string query = "SELECT idArticulos, codigo, nombre, cantidad, precio, descripcion, nombreGrupo FROM Articulos us inner join Grupos pr on us.idGrupo = pr.idGrupo order by idArticulos ASC";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter data = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            GridDatosProductos.ItemsSource = dt.DefaultView;
            con.Close();

        }

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            CRUDProductos ventana = new CRUDProductos();
            FrameProductos.Content = ventana;
            Contenido.Visibility = Visibility.Hidden;
            ventana.btnCrearProducto.Visibility = Visibility.Visible;
        }

        private void Consultar(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDProductos ventana = new CRUDProductos();
            ventana.idArticulo = id;
            ventana.Consultar();
            FrameProductos.Content = ventana;
            ventana.titulo.Text = "Consulta de producto";
            ventana.tbCodigo.IsEnabled = false;
            ventana.tbNombreProducto.IsEnabled = false;
            ventana.tbCantidad.IsEnabled = false;
            ventana.tbPrecio.IsEnabled = false;
            ventana.tbDescripcion.IsEnabled = false;
            ventana.cbGrupo.IsEnabled = false;
            // ventana.tbFecha.IsEnabled = false;
            ventana.tbMedida.IsEnabled = false;
            ventana.btnSubirProducto.IsEnabled = false;
            Contenido.Visibility = Visibility.Hidden;

        }

        private void ActualizarProducto(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDProductos ventana = new CRUDProductos();
            ventana.idArticulo = id;
            ventana.Consultar();
            FrameProductos.Content = ventana;
            ventana.titulo.Text = "Actualizar Producto";
            ventana.tbCodigo.IsEnabled = true;
            ventana.tbNombreProducto.IsEnabled = true;
            ventana.tbCantidad.IsEnabled = true;
            ventana.tbPrecio.IsEnabled = true;
            ventana.tbDescripcion.IsEnabled = true;
            ventana.cbGrupo.IsEnabled = true;
            //ventana.tbFecha.IsEnabled = true;
            ventana.tbMedida.IsEnabled = true;
            ventana.btnSubirProducto.IsEnabled = true;
            ventana.btnActualizarProducto.Visibility = Visibility.Visible;
            Contenido.Visibility = Visibility.Hidden;
        }
        private void EliminarProducto(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            CRUDProductos ventana = new CRUDProductos();
            ventana.idArticulo = id;
            ventana.Consultar();
            FrameProductos.Content = ventana;
            ventana.titulo.Text = "Eliminar producto";
            ventana.tbCodigo.IsEnabled = false;
            ventana.tbNombreProducto.IsEnabled = false;
            ventana.tbCantidad.IsEnabled = false;
            ventana.tbPrecio.IsEnabled = false;
            ventana.tbDescripcion.IsEnabled = false;
            ventana.cbGrupo.IsEnabled = false;
            // ventana.tbFecha.IsEnabled = false;
            ventana.tbMedida.IsEnabled = false;
            ventana.btnEliminarProducto.Visibility = Visibility.Visible;
            Contenido.Visibility = Visibility.Hidden;

        }
        #region Buscando
        private void BuscandoProducto(object sender, TextChangedEventArgs e)
        {
            string buscar = TextBoxBuscarProducto.Text;
            {
                con.Open();
                string queryBuscar = "SELECT Articulos.*, Grupos.nombreGrupos" +
                                     "FROM Usuarios " +
                                     "INNER JOIN Grupos ON Articulos.idGrupo = Grupos.idGrupos " +
                                     "WHERE codigo LIKE @buscar OR nombre LIKE @buscar ";
                MySqlCommand cmd = new MySqlCommand(queryBuscar, con);
                cmd.Parameters.AddWithValue("@buscar", buscar + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                GridDatosProductos.ItemsSource = dataTable.DefaultView;
                con.Close();
            }

        }
        #endregion
        #endregion
    }
}