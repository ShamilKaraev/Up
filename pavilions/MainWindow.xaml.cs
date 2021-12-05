using System.Windows;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace pavilions {

    public partial class MainWindow : Window {
        public MainWindow() {
            try {
                InitializeComponent();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder() {
                    DataSource = @"DESKTOP-52L8N5J\SQLEXPRESS02",
                    InitialCatalog = @"Pavilions",
                    IntegratedSecurity = true
                };
                Manager.connection = new SqlConnection(builder.ConnectionString);
                MAINFRAME.Navigate(new LogIn());
                Manager.MainFrame = MAINFRAME;
            }
            catch (SqlException error) {
                MessageBox.Show(error.Message);
            }
        }



















        private void exportone(object sender, RoutedEventArgs e) {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
                DataSource = @"DESKTOP-52L8N5J\SQLEXPRESS02",
                InitialCatalog = "PAVILIONS",
                IntegratedSecurity = true
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\Sergo\Desktop\Знания\III курс\6СЕМЕСТР\Ларионова0202\УП0201\Ресурсы\Image ТЦ");
            FileInfo[] files = d.GetFiles("*.jpg");
            SqlCommand cmd;
            connection.Open();
            for (int i = 0; i < files.Length; i++) {
                cmd = new SqlCommand("UPDATE SHOPING_CENTER SET image = @image where shop_center_name = @name", connection);
                cmd.Parameters.AddWithValue("@image", File.ReadAllBytes(files[i].FullName));
                cmd.Parameters.AddWithValue("@name", files[i].Name.Remove(files[i].Name.Length - 4, 4));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void exporttwo(object sender, RoutedEventArgs e) {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
                DataSource = @"DESKTOP-52L8N5J\SQLEXPRESS02",
                InitialCatalog = "PAVILIONS",
                IntegratedSecurity = true
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\Sergo\Desktop\Знания\III курс\6СЕМЕСТР\Ларионова0202\УП0201\Ресурсы\Image Сотрудники");
            FileInfo[] files = d.GetFiles("*.jpg");
            SqlCommand cmd;
            connection.Open();
            for (int i = 0; i < files.Length; i++) {
                cmd = new SqlCommand("UPDATE EMPLOYEES SET photo = @image where surname_employee = @name", connection);
                cmd.Parameters.AddWithValue("@image", File.ReadAllBytes(files[i].FullName));
                cmd.Parameters.AddWithValue("@name", files[i].Name.Remove(files[i].Name.Length - 4, 4));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}