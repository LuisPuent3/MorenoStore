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
<<<<<<< Updated upstream:CRUD STORE/Vistas/Usuarios.xaml.cs

=======
      
>>>>>>> Stashed changes:Base-de-datos-tienda/CRUD STORE/CRUD STORE/Vistas/Usuarios.xaml.cs
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
            FrameUsurios.Content = ventana;
        }
    }
}
