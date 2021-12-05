using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace pavilions {
    public partial class SC_Details : Page {

        public class SCs {
            public int shop_center_id { get; set; }
            public string shop_center_name { get; set; }
            public string status { get; set; }
            public int count_pavilions { get; set; }
            public string city { get; set; }
            public string price { get; set; }
            public string var_coefficient { get; set; }
            public int floor { get; set; }
        }
        public class SCs_image {
            public byte[] CS_Image { get; set; }

        }
        public int SC_id { get; set; }
        public int Employee_id { get; set; }

        public SC_Details(int shop_center_id, int employee_id) {
            try {
                Employee_id = employee_id;
                SC_id = shop_center_id;
                InitializeComponent();
                Manager.connection.Close();
                Manager.connection.Open();
                List<SCs> SCs_List = new List<SCs>();
                List<SCs_image> SCs_image_List = new List<SCs_image>();

                SqlCommand command = new SqlCommand("SELECT * FROM SHOPING_CENTER WHERE status <> 'Удален' AND shop_center_id = @shop_center_id_value", Manager.connection);
                SqlParameter shop_center_id_param = new SqlParameter("@shop_center_id_value", shop_center_id); command.Parameters.Add(shop_center_id_param);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        SCs tmp = new SCs();
                        tmp.shop_center_id = reader.GetInt32(0);
                        tmp.shop_center_name = reader[1].ToString();
                        tmp.status = reader[2].ToString();
                        tmp.count_pavilions = reader.GetInt32(3);
                        tmp.city = reader[4].ToString();
                        tmp.price = reader.GetValue(5).ToString();
                        tmp.var_coefficient = reader.GetValue(6).ToString();
                        tmp.floor = reader.GetInt32(7);

                        SCs_image im = new SCs_image();
                        im.CS_Image = reader[8] as byte[];

                        SCs_List.Add(tmp);
                        SCs_image_List.Add(im);
                    }
                    SCs_Details_Grid.ItemsSource = SCs_List;
                    Image_SC.DataContext = SCs_image_List;

                }

                Manager.connection.Close();
            }
            catch (SqlException error) { MessageBox.Show(error.Message); }
        }


        private void Write_Pavilions(object sender, RoutedEventArgs e) {          
            try {
                Manager.connection.Open();
                SqlCommand command = new SqlCommand("SELECT dbo.SHOPING_CENTER.status, dbo.SHOPING_CENTER.shop_center_name, dbo.PAVILIONS.floor, dbo.PAVILIONS.id_pavilion, dbo.PAVILIONS.area, dbo.PAVILIONS.status AS Expr1, dbo.PAVILIONS.var_coefficient, " +
                " dbo.PAVILIONS.price FROM dbo.PAVILIONS INNER JOIN dbo.SHOPING_CENTER ON dbo.PAVILIONS.id_shop_center = dbo.SHOPING_CENTER.shop_center_id WHERE id_shop_center=@shop_center_id_value", Manager.connection);
                SqlParameter shop_center_id_param = new SqlParameter("@shop_center_id_value", SC_id); command.Parameters.Add(shop_center_id_param);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {

                    Manager.connection.Close();
                    Manager.MainFrame.Navigate(new ManagerC_SCs_Pavilions(SC_id, Employee_id));

                } else {

                    Manager.connection.Close();
                    MessageBox.Show("Павильоны отсутствуют");
                }
            }
            catch (SqlException error) { MessageBox.Show(error.Message); }
        }


        private void Exit(object sender, RoutedEventArgs e) {
            Manager.MainFrame.GoBack();
        }

        private void Delete_SC_Button_ManagerC_Details(object sender, RoutedEventArgs e) {
            try {
                Manager.connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM SHOPING_CENTER WHERE shop_center_id = @shop_center_id_value", Manager.connection);
                SqlParameter shop_center_id_param = new SqlParameter("@shop_center_id_value", SC_id); command.Parameters.Add(shop_center_id_param);
                command.ExecuteNonQuery();
                Manager.connection.Close();
                MessageBox.Show("ТЦ удален");
                Manager.MainFrame.Navigate(new ManagerC(Employee_id));
            }
            catch (SqlException error) { MessageBox.Show(error.Message); }
        }

        private void Edit_SC_Button_ManagerC_Edit(object sender, RoutedEventArgs e) {
            if (Name.Text.Length > 0 && Status.Text.Length > 0 && Count_pavilions.Text.Length > 0 && City.Text.Length > 0 && Price.Text.Length > 0 && Var_coefficient.Text.Length > 0 && Floor.Text.Length > 0) {
                try {
                    Manager.connection.Close();
                    Manager.connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE SHOPING_CENTER SET shop_center_id = (SELECT MAX(shop_center_id) + 1 AS shop_center_id1 FROM dbo.SHOPING_CENTER), shop_center_name = @shop_center_name_value, status = @status_value, count_pavilions = @count_pavilions_value, city = @city_value, price = @price_value, var_coefficient = @var_coefficient_value, floor = @floor_value WHERE shop_center_id = @shop_center_id_value", Manager.connection);
                    SqlParameter shop_center_id_param = new SqlParameter("@shop_center_id_value", SC_id); command.Parameters.Add(shop_center_id_param);
                    SqlParameter shop_center_name_param = new SqlParameter("@shop_center_name_value", Name.Text); command.Parameters.Add(shop_center_name_param);
                    SqlParameter status_param = new SqlParameter("@status_value", Status.Text); command.Parameters.Add(status_param);
                    SqlParameter count_pavilions_param = new SqlParameter("@count_pavilions_value", Count_pavilions.Text); command.Parameters.Add(count_pavilions_param);
                    SqlParameter city_param = new SqlParameter("@city_value", City.Text); command.Parameters.Add(city_param);
                    SqlParameter price_param = new SqlParameter("@price_value", Price.Text); command.Parameters.Add(price_param);
                    SqlParameter var_coefficient_param = new SqlParameter("@var_coefficient_value", Var_coefficient.Text); command.Parameters.Add(var_coefficient_param);
                    SqlParameter floor_param = new SqlParameter("@floor_value", Floor.Text); command.Parameters.Add(floor_param);
                    command.ExecuteNonQuery();
                    Manager.connection.Close();
                    MessageBox.Show("Данные обновлены");
                    White_Wall.Visibility = Visibility.Visible;
                    Dell_Button_ManagerC_Details.Visibility = Visibility.Visible;
                    Add_Button_ManagerC_Details.Visibility = Visibility.Visible;
                    Edit_Button_ManagerC_Details.Visibility = Visibility.Visible;
                }
                catch (SqlException error) { MessageBox.Show(error.Message); }
            } else MessageBox.Show("Заполните все поля");
        }
        private void Edit_SC_Button_ManagerC_Details(object sender, RoutedEventArgs e) {

            White_Wall.Visibility = Visibility.Hidden;
            Dell_Button_ManagerC_Details.Visibility = Visibility.Hidden;
            Add_Button_ManagerC_Details.Visibility = Visibility.Hidden;
            Edit_Button_ManagerC_Details.Visibility = Visibility.Hidden;
            Edit_Button_ManagerC_Details_Run.Visibility = Visibility.Visible;
            Add_Button_ManagerC_Details_Run.Visibility = Visibility.Hidden;

        }

        private void Add_new_SC_Button_ManagerC_Add(object sender, RoutedEventArgs e) {
            if (Name.Text.Length > 0 && Status.Text.Length > 0 && Count_pavilions.Text.Length > 0 && City.Text.Length > 0 && Price.Text.Length > 0 && Var_coefficient.Text.Length > 0 && Floor.Text.Length > 0) {
                try {
                    Manager.connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO SHOPING_CENTER (shop_center_id,shop_center_name,status,count_pavilions,city,price,var_coefficient,floor,image) VALUES " +
                    " ((SELECT MAX(shop_center_id) + 1 AS shop_center_id1 FROM dbo.SHOPING_CENTER),@shop_center_name_value,@status_value,@count_pavilions_value,@city_value,@price_value,@var_coefficient_value,@floor_value,NULL)", Manager.connection);
                    SqlParameter shop_center_name_param = new SqlParameter("@shop_center_name_value", Name.Text); command.Parameters.Add(shop_center_name_param);
                    SqlParameter status_param = new SqlParameter("@status_value", Status.Text); command.Parameters.Add(status_param);
                    SqlParameter count_pavilions_param = new SqlParameter("@count_pavilions_value", Count_pavilions.Text); command.Parameters.Add(count_pavilions_param);
                    SqlParameter city_param = new SqlParameter("@city_value", City.Text); command.Parameters.Add(city_param);
                    SqlParameter price_param = new SqlParameter("@price_value", Price.Text); command.Parameters.Add(price_param);
                    SqlParameter var_coefficient_param = new SqlParameter("@var_coefficient_value", Var_coefficient.Text); command.Parameters.Add(var_coefficient_param);
                    SqlParameter floor_param = new SqlParameter("@floor_value", Floor.Text); command.Parameters.Add(floor_param);
                    command.ExecuteNonQuery();
                    Manager.connection.Close();
                    MessageBox.Show("Данные добавлены");
                    White_Wall.Visibility = Visibility.Visible;
                    Dell_Button_ManagerC_Details.Visibility = Visibility.Visible;
                    Add_Button_ManagerC_Details.Visibility = Visibility.Visible;
                    Edit_Button_ManagerC_Details.Visibility = Visibility.Visible;
                }
                catch (SqlException error) { MessageBox.Show(error.Message); }
            } else MessageBox.Show("Заполните все поля");
        }
        private void Add_new_SC_Button_ManagerC_Details(object sender, RoutedEventArgs e) {
            White_Wall.Visibility = Visibility.Hidden;
            Dell_Button_ManagerC_Details.Visibility = Visibility.Hidden;
            Add_Button_ManagerC_Details.Visibility = Visibility.Hidden;
            Edit_Button_ManagerC_Details.Visibility = Visibility.Hidden;
            Edit_Button_ManagerC_Details_Run.Visibility = Visibility.Hidden;
            Add_Button_ManagerC_Details_Run.Visibility = Visibility.Visible;
        }
    }
}
