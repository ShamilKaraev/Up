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
    public partial class LogIn : Page {

        public int attempt;
        
        public LogIn() {
            InitializeComponent();
            attempt = 0;
        }

        private void LogIn_Button(object sender, RoutedEventArgs e) {
            Error_Textdlock.Text = (login_textbox.Text.Length > 0) ? "" : "Незаполнено поле логин \n";
            Error_Textdlock.Text += (password_box.Password.Length > 0) ? "" : "Незаполнено поле пароль \n";
            
            if (Error_Textdlock.Text.Length == 0) {
                Manager.connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM EMPLOYEES WHERE login = @login_value AND password COLLATE Cyrillic_General_CS_AS = @password_value", Manager.connection);
                SqlParameter login_param = new SqlParameter("@login_value", login_textbox.Text); command.Parameters.Add(login_param);
                SqlParameter password_param = new SqlParameter("@password_value", password_box.Password); command.Parameters.Add(password_param);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    reader.Read();
                    int employee_id = reader.GetInt32(0);
                    string role = reader[8].ToString();


                    switch (role) {
                        case "Администратор": {
                                attempt = 0;
                                Manager.MainFrame.Navigate(new Admin());
                            }
                            break;
                        case "Менеджер А": {
                                attempt = 0;
                                Manager.MainFrame.Navigate(new ManagerA());
                            }
                            break;
                        case "Менеджер С": {
                                attempt = 0;
                                Manager.MainFrame.Navigate(new ManagerC(employee_id));
                            }
                            break;
                    }

                } else {
                    Error_Textdlock.Text = "Неправильный логин или пароль"; 
                    attempt++;
                }

                Manager.connection.Close();
            } else { attempt++; }
            if (attempt >= 3) Manager.MainFrame.Navigate(new Captcha());
        }
    }
}
