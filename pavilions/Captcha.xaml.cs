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

namespace pavilions {
    public partial class Captcha : Page {
        public void Generate_Captcha() {
            Captcha_Textblock.Text = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++) Captcha_Textblock.Text += Convert.ToChar(random.Next(65, 90)).ToString();
        }
        public Captcha() {
            InitializeComponent();
            Generate_Captcha();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (Captcha_Textbox.Text == Captcha_Textblock.Text) {
                Manager.MainFrame.Navigate(new LogIn());
            } else {
                MessageBox.Show("Неверно, попробуйте еще раз");
                Generate_Captcha();
            }
        }
    }
}
