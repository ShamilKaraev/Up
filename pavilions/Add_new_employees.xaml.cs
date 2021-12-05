using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace pavilions {
    public partial class Add_new_employees : Page {
        public byte[] imageData = null;
        public Add_new_employees() {
            InitializeComponent();
        }

        private void Add_new_employee(object sender, RoutedEventArgs e) {
            if ((Name_TextBox.Text.Length > 0) && (Surname_TextBox.Text.Length > 0) && (Middle_name_TextBox.Text.Length > 0) && (Phone_TextBox.Text.Length > 0) && (Gender_ComboBox.Text.Length > 0) && (password_TextBox.Text.Length > 0) && (Role_ComboBox.Text.Length > 0) && (Photo_TextBox.Text.Length > 0)) {
                try {
                    Manager.connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO EMPLOYEES (id_employee,surname_employee,name_employee,mid_name_employee,phone,gender,login,password,role,photo) VALUES " +
                    " ((SELECT MAX(id_employee) + 1 AS id_employee1 FROM dbo.EMPLOYEES),@surname_employee,@name_employee,@mid_name_employee,@phone,@gender,@login,@password,@role,@photo)", Manager.connection);
                    SqlParameter surname_employee = new SqlParameter("@surname_employee", Surname_TextBox.Text); command.Parameters.Add(surname_employee);
                    SqlParameter name_employee = new SqlParameter("@name_employee", Name_TextBox.Text); command.Parameters.Add(name_employee);
                    SqlParameter mid_name_employee = new SqlParameter("@mid_name_employee", Middle_name_TextBox.Text); command.Parameters.Add(mid_name_employee);
                    SqlParameter phone = new SqlParameter("@phone", Phone_TextBox.Text); command.Parameters.Add(phone);
                    SqlParameter gender = new SqlParameter("@gender", Gender_ComboBox.Text); command.Parameters.Add(gender);
                    SqlParameter login = new SqlParameter("@login", login_TextBox.Text); command.Parameters.Add(login);
                    SqlParameter password = new SqlParameter("@password", password_TextBox.Text); command.Parameters.Add(password);
                    SqlParameter role = new SqlParameter("@role", Role_ComboBox.Text); command.Parameters.Add(role);
                    SqlParameter photo = new SqlParameter("@photo", (object)imageData); command.Parameters.Add(photo);
                    command.ExecuteNonQuery();
                    Manager.connection.Close();
                    MessageBox.Show("Данные добавлены");
                    Manager.MainFrame.Navigate(new Admin());
                }
                catch (SqlException error) {
                    MessageBox.Show(error.Message);
                }
            } else MessageBox.Show("Заполните поля");
        }

        private void Admin_Image_Button(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.Filter = "Картинки(*.JPG;*.GIF;*.png;)|*.JPG;*.GIF;*.png;" + "|Все файлы (*.*)|*.* ";
                myDialog.CheckFileExists = true;
                myDialog.Multiselect = true;
                if (myDialog.ShowDialog() == true) {
                    FileInfo fInfo = new FileInfo(myDialog.FileName);
                    long numBytes = fInfo.Length;

                    Photo_TextBox.Text = myDialog.FileName;
                    FileStream FileStream = new FileStream(myDialog.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader BinaryReader = new BinaryReader(FileStream);
                    imageData = BinaryReader.ReadBytes((int)numBytes);

                }
            }
            catch (SqlException error) {
                MessageBox.Show(error.Message);
            }
        }

        private void Exit(object sender, RoutedEventArgs e) {
            Manager.MainFrame.Navigate(new LogIn());
        }
    }
}
