using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace pavilions {
    public class newclass { 
        public string name { get; set; }
        public string price { get; set; } 
    }
    public partial class ManagerA : Page {
        public ManagerA() {
            InitializeComponent();
            Manager.connection.Close();
            Manager.connection.Open();
            List<newclass> newclasses_list = new List<newclass>();
            SqlCommand command = new SqlCommand("EXEC Procedure2",Manager.connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) {
                while (reader.Read()) {
                    newclasses_list.Add(new newclass {
                        name = reader[0].ToString(),
                        price = reader[1].ToString()
                    });
                    
                }
                ManagerA_Grid.ItemsSource = newclasses_list;
            }
            Manager.connection.Close();
        }

        private void Exit(object sender, RoutedEventArgs e) {
            Manager.MainFrame.Navigate(new LogIn());
        }

    }
}
