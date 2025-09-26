using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrudClientes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly string CNX =
          @"Server=localhost\SQLEXPRESS;Database=NeptunoDBB;User ID=userHugo;Password=123456;Encrypt=True;TrustServerCertificate=True;";
        public MainWindow()
        {

            InitializeComponent();
        }
        // INSERT
        private void BtnInsertar_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                using var cn = new SqlConnection(CNX);
                using var cmd = new SqlCommand("usp.Clientes_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Usa el mismo nombre de parámetro que definiste en tus SP.
                cmd.Parameters.AddWithValue("@idCliente", txtId.Text.Trim());
                cmd.Parameters.AddWithValue("@NombreCompañía", txtCompania.Text.Trim()); // si tu SP usa tilde: @NombreCompañía
                cmd.Parameters.AddWithValue("@NombreContacto", (object?)txtContacto.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CargoContacto", (object?)txtCargo.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Direccion", (object?)txtDireccion.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ciudad", (object?)txtCiudad.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Region", (object?)txtRegion.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CodPostal", (object?)txtPostal.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Pais", (object?)txtPais.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object?)txtTelefono.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fax", (object?)txtFax.Text.Trim() ?? DBNull.Value);

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insertado.");
            }
            catch (SqlException ex) { MessageBox.Show("SQL: " + ex.Message); }

        }


        // GET BY ID
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var cn = new SqlConnection(CNX);
                using var cmd = new SqlCommand("usp.Clientes_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using var dr = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(dr);
                dgResultado.ItemsSource = dt.DefaultView;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL: " + ex.Message);
            }
            
        }

        // UPDATE
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var cn = new SqlConnection(CNX);
                using var cmd = new SqlCommand("usp.Clientes_Update", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCliente", txtId.Text.Trim());
                cmd.Parameters.AddWithValue("@NombreCompania", txtCompania.Text.Trim()); // o @NombreCompañia si tu SP lo usa así
                cmd.Parameters.AddWithValue("@NombreContacto", (object?)txtContacto.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CargoContacto", (object?)txtCargo.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Direccion", (object?)txtDireccion.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ciudad", (object?)txtCiudad.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Region", (object?)txtRegion.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CodPostal", (object?)txtPostal.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Pais", (object?)txtPais.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object?)txtTelefono.Text.Trim() ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fax", (object?)txtFax.Text.Trim() ?? DBNull.Value);

                cn.Open();
                int n = cmd.ExecuteNonQuery();
                MessageBox.Show(n > 0 ? "Actualizado." : "Nada que actualizar.");
            }
            catch (SqlException ex) { MessageBox.Show("SQL: " + ex.Message); }
        }

        // DELETE
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var cn = new SqlConnection(CNX);
                using var cmd = new SqlCommand("usp.Clientes_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", txtId.Text.Trim());

                cn.Open();
                int n = cmd.ExecuteNonQuery();
                MessageBox.Show(n > 0 ? "Eliminado." : "No existe ese Id.");
            }
            catch (SqlException ex) { MessageBox.Show("SQL: " + ex.Message); }
        }
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            // Limpia todos los campos
            txtId.Clear();
            txtCompania.Clear();
            txtContacto.Clear();
            txtCargo.Clear();
            txtDireccion.Clear();
            txtCiudad.Clear();
            txtRegion.Clear();
            txtPostal.Clear();
            txtPais.Clear();
            txtTelefono.Clear();
            txtFax.Clear();

            // Limpia la grilla y enfoca el Id
            dgResultado.ItemsSource = null;
            txtId.Focus();
        }


    }
}