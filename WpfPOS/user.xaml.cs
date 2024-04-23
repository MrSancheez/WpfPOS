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
using System.Windows.Shapes;

namespace WpfPOS
{
    /// <summary>
    /// Interaction logic for user.xaml
    /// </summary>
    public partial class user : Window
    {
        public user()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new POS();
        }

        private void Staff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Staff();
        }

        private void POS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new SalesSystem();
        }

        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Logout_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
