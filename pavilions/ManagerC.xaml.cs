using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace pavilions {
    public partial class ManagerC : Page {

        public class SCs {
            public int shop_center_id { get; set; }
            public string shop_center_name { get; set; }
            public string status { get; set; }
            public int count_pavilions { get; set; }
            public string city { get; set; }
            public string price { get; set; }
            public string var_coefficient { get; set; }
            public int floor { get; set; }
            public byte[] image { get; set; }

        }
        public string Command_ManagerC_SCs_Grid;
        public int Employee_id;

        public void Write_SCs() {
            try {
                InitializeComponent();
                Manager.connection.Close();
                Manager.connection.Open();
                List<SCs> SCs_List = new List<SCs>();
                SqlCommand command = new SqlCommand(Command_ManagerC_SCs_Grid, Manager.connection);
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
                        tmp.image = reader[8] as byte[];

                        SCs_List.Add(tmp);
                    }
                    ManagerC_SCs_Grid.ItemsSource = SCs_List;
                    ManagerC_SCs_Grid.AutoGenerateColumns = false;
                    ManagerC_SCs_Grid.Columns[2].Visibility = Visibility.Hidden;
                }
                Manager.connection.Close();
            }
            catch (SqlException error) { MessageBox.Show(error.Message); }
        }

        public ManagerC(int employee_id) {
            Employee_id = employee_id;
            Command_ManagerC_SCs_Grid = "SELECT * FROM SHOPING_CENTER WHERE status <> 'Удален'";
            Write_SCs();
        }

        private void SC_Details_Button(object sender, RoutedEventArgs e) {
            SCs SCs = (SCs)ManagerC_SCs_Grid.SelectedItem;
            Manager.MainFrame.Navigate(new SC_Details(SCs.shop_center_id, Employee_id));
        }

        private void Sort_CSs_ByName_Button(object sender, RoutedEventArgs e) {
            Command_ManagerC_SCs_Grid = "SELECT * FROM SHOPING_CENTER WHERE status <> 'Удален' ORDER BY city";
            Write_SCs();
        }

        private void Sort_CSs_ByStatus_Button(object sender, RoutedEventArgs e) {
            Command_ManagerC_SCs_Grid = "SELECT * FROM SHOPING_CENTER WHERE status <> 'Удален' ORDER BY status";
            Write_SCs();
        }

        private void Clear_ManagerC_SCs_Grid(object sender, RoutedEventArgs e) {
            Command_ManagerC_SCs_Grid = "SELECT * FROM SHOPING_CENTER WHERE status <> 'Удален'";
            Write_SCs();
        }

        private void Exit(object sender, RoutedEventArgs e) {
            Manager.MainFrame.Navigate(new LogIn());
        }


        private void Filter_CSs_ByName_Button(object sender, RoutedEventArgs e) {
            if (ManagerC_City_textbox.Text.Length > 0) {
                string str = ManagerC_City_textbox.Text;
                str = str.Replace("'","['']");
                str = str.Replace(";", "[;]");
                Command_ManagerC_SCs_Grid = "SELECT * FROM SHOPING_CENTER WHERE city like '%" + str + "%'";
                Write_SCs();
            }
        }

        private void Filter_CSs_ByStatus_Button(object sender, RoutedEventArgs e) {
            if (filter_status.SelectedItem != null) {
                Command_ManagerC_SCs_Grid = "SELECT * FROM SHOPING_CENTER WHERE status = '" + filter_status.Text + "'";
                Write_SCs();
            }
        }

    }
}
