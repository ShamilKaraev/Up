using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace pavilions {
    public partial class Admin : Page {
        public class Employees {
            public int id_employee { get; set; }
            public string surname_employee { get; set; }
            public string name_employee { get; set; }
            public string mid_name_employee { get; set; }
            public string phone { get; set; }
            public string gender { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public string role { get; set; }
            public byte[] photo { get; set; }
        }
        public Admin() {
            InitializeComponent();
            try {
                Manager.connection.Close();
                Manager.connection.Open();
                List<Employees> Employees_list = new List<Employees>();
                SqlCommand command = new SqlCommand("SELECT * FROM EMPLOYEES",Manager.connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {

                        Employees_list.Add(new Employees {
                            id_employee = reader.GetInt32(0),
                            surname_employee = reader[1].ToString(),
                            name_employee = reader[2].ToString(),
                            mid_name_employee = reader[3].ToString(),
                            phone = reader[4].ToString(),
                            gender = reader[5].ToString(),
                            login = reader[6].ToString(),
                            password = reader[7].ToString(),
                            role = reader[8].ToString(),
                            photo = reader.GetValue(9) as byte[]
                        });
                    }
                    Employees_Grid.ItemsSource = Employees_list;
                    Employees_Grid.AutoGenerateColumns = false;
                }

                Manager.connection.Close();
            } catch (SqlException err) { MessageBox.Show(err.Message); }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            //MessageBox.Show(Search_TextBox.Text);
        }

        private void Add_employee(object sender, RoutedEventArgs e) {
            Manager.MainFrame.Navigate(new Add_new_employees());
        }

        private void Edit_employee(object sender, RoutedEventArgs e) {
            if (Employees_Grid.SelectedItem != null) { 
                Employees employees_selected = (Employees)Employees_Grid.SelectedItem;
                Manager.MainFrame.Navigate(new Edit_old_employees(employees_selected.id_employee));
            } else MessageBox.Show("Выбирите сотрудника");
        }

        private void Dell_employee(object sender, RoutedEventArgs e) {
            try {
                if (Employees_Grid.SelectedItem != null) {
                    Employees employees_selected = (Employees)Employees_Grid.SelectedItem;
                    Manager.connection.Open();

                    SqlCommand command = new SqlCommand("DELETE FROM EMPLOYEES WHERE id_employee = @id_employee_value", Manager.connection);
                    SqlParameter id_employee_param = new SqlParameter("@id_employee_value", employees_selected.id_employee); command.Parameters.Add(id_employee_param);
                    command.ExecuteNonQuery();
                    Manager.connection.Close();

                    MessageBox.Show("Сотрудник удален");

                } else MessageBox.Show("Выбирите сотрудника");

            } catch (SqlException error) { MessageBox.Show(error.Message); }
        }

        private void Exit(object sender, RoutedEventArgs e) {
            Manager.MainFrame.Navigate(new LogIn());
        }
    }
}
