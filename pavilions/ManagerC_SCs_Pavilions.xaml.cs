using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace pavilions {
    public partial class ManagerC_SCs_Pavilions : Page {
        public class Pavilions {
            public string status_CS {get; set; }
            public string name_SC {get; set; }
            public int floor { get; set; }
            public string id_pavilion { get; set; }
            public int area { get; set; }
            public string status_pavilion { get; set; }
            public string var_coefficient { get; set; }
            public string price { get; set; }
        }
        public class Rentors {
            public int renter_id {get; set; }
            public string Name {get; set; }
            public string Phone {get; set; }
            public string City {get; set; }
            public string Street {get; set; }
        }
        public string Id_pavilion { get; set; }
        public int Renter_id { get; set; }
        public int SC_id { get; set; }
        public int Employee_id { get; set; }
        public ManagerC_SCs_Pavilions(int SCs_shop_center_id,int employee_id) {
            try {
                SC_id = SCs_shop_center_id;
                Employee_id = employee_id;
                InitializeComponent();
                Change_Rentor_Button.Visibility = Visibility.Hidden;
                ManagerC_Rentors_Grid.Visibility = Visibility.Hidden;
                Manager.connection.Open();

                List<Pavilions> Pavilions_List = new List<Pavilions>();

                SqlCommand command = new SqlCommand("SELECT dbo.SHOPING_CENTER.status, dbo.SHOPING_CENTER.shop_center_name, dbo.PAVILIONS.floor, dbo.PAVILIONS.id_pavilion, dbo.PAVILIONS.area, dbo.PAVILIONS.status AS Expr1, dbo.PAVILIONS.var_coefficient, " +
                " dbo.PAVILIONS.price FROM dbo.PAVILIONS INNER JOIN dbo.SHOPING_CENTER ON dbo.PAVILIONS.id_shop_center = dbo.SHOPING_CENTER.shop_center_id WHERE id_shop_center=@shop_center_id_value AND PAVILIONS.[status]='Свободен'", Manager.connection);
                SqlParameter shop_center_id_param = new SqlParameter("@shop_center_id_value", SCs_shop_center_id); command.Parameters.Add(shop_center_id_param);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        Pavilions tmp = new Pavilions();

                        tmp.status_CS = reader[0].ToString();
                        tmp.name_SC = reader[1].ToString();
                        tmp.floor = reader.GetInt32(2);
                        tmp.id_pavilion = reader[3].ToString();
                        tmp.area = reader.GetInt32(4);
                        tmp.status_pavilion = reader[5].ToString();
                        tmp.var_coefficient = reader.GetValue(6).ToString();
                        tmp.price = reader.GetValue(7).ToString();

                        Pavilions_List.Add(tmp);
                    }

                    Manager.connection.Close();
                    ManagerC_Pavilions_Grid.ItemsSource = Pavilions_List;

                } else {
                    Manager.connection.Close();
                    //Manager.MainFrame.Navigate(new ManagerC());
                    MessageBox.Show("Павильоны отсутствуют");
                }



                Manager.connection.Open();

                List<Rentors> Rentors_List = new List<Rentors>();

                command = new SqlCommand("SELECT * FROM RENTORS", Manager.connection);
                reader = command.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        Rentors tmp = new Rentors();

                        tmp.renter_id = reader.GetInt32(0);
                        tmp.Name = reader[1].ToString();
                        tmp.Phone = reader[2].ToString();
                        tmp.City = reader[3].ToString();
                        tmp.Street = reader[4].ToString();

                        Rentors_List.Add(tmp);
                    }

                    Manager.connection.Close();
                    ManagerC_Rentors_Grid.ItemsSource = Rentors_List;

                } else {
                    Manager.connection.Close();
                    //Manager.MainFrame.Navigate(new ManagerC());
                    MessageBox.Show("Павильоны отсутствуют");
                }
            }
            catch (SqlException error) { MessageBox.Show(error.Message); }
            
        }

        private void Come_Back(object sender, RoutedEventArgs e) {
            Manager.MainFrame.Navigate(new ManagerC(Employee_id));
        }

        private void Change_Pavilion(object sender, RoutedEventArgs e) {
            Pavilions Pavilions = (Pavilions)ManagerC_Pavilions_Grid.SelectedItem;
            if (Pavilions != null) {
                Change_Rentor_Button.Visibility = Visibility.Visible;
                ManagerC_Rentors_Grid.Visibility = Visibility.Visible;
                Change_Pavilion_Button.Visibility = Visibility.Hidden;
                ManagerC_Pavilions_Grid.Visibility = Visibility.Hidden;
                Id_pavilion = Pavilions.id_pavilion.ToString();
            }
        }

        private void Change_Rentor(object sender, RoutedEventArgs e) {
            Rentors Rentors = (Rentors)ManagerC_Rentors_Grid.SelectedItem;
            if (Rentors != null) {
                Renter_id = Rentors.renter_id;
                Change_Rentor_Button.Visibility = Visibility.Hidden;
                ManagerC_Rentors_Grid.Visibility = Visibility.Hidden;
                Add_Rent_Button.Visibility = Visibility.Visible;
            }
        }

        private void Add_Rentor(object sender, RoutedEventArgs e) {
            if (rent_start.SelectedDate<rent_end.SelectedDate) {

                try {
                    InitializeComponent();
                    int top = 1;
                    Manager.connection.Open();
                    SqlCommand command = new SqlCommand("SELECT MAX(rent_id) + 1 AS id_rent FROM RENTS", Manager.connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) {
                        reader.Read();
                        top = Convert.ToInt32(reader[0].ToString());

                    }

                    Manager.connection.Close();
                    Manager.connection.Open();

                    command = new SqlCommand("exec NEW_RENT @id_rent = " + top + ", @id_renter = " + Renter_id + " , " +
                    " @shop_center = " + SC_id + " , @id_employee = " + Employee_id + " , " +
                    " @id_pavilion = '" + Id_pavilion + "' , @date_start = '" + rent_start.Text + "' , @date_end = '" + rent_end.Text + "'", Manager.connection);
                    command.ExecuteNonQuery();
                    Manager.connection.Close();

                    MessageBox.Show("Заказ оформлен");
                    Manager.MainFrame.Navigate(new ManagerC(Employee_id));

                } catch (SqlException error) { MessageBox.Show(error.Message); }


            }
        }
    }
}
