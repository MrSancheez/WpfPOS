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
using System.Data;

namespace WpfPOS
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection con;
        public Login()
        {
            InitializeComponent();
            Connect connect = new Connect();
            connect.setConnection();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string usr = username.Text;
            string pass = password.Password;

            if (string.IsNullOrEmpty(usr))
            {
                // MessageBox.Show("Please Enter Username");
                error.Visibility = Visibility.Visible;
                error.Content = "Plese Enter Username";
                
            }
            else if (string.IsNullOrEmpty(pass))
            {
                // MessageBox.Show("Please Enter Password");
                error.Visibility = Visibility.Visible;
                error.Content = "Please Enter Password";
            }
            else
            {
                error.Visibility = Visibility.Hidden;
                Connect.con.Open();
                String query = "SELECT * FROM Users where UserName = '" + usr + "' and Password='" + pass + "'";

                SqlCommand cmd = new SqlCommand(query, Connect.con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read()) 
                {
                    if(dr[6].ToString()=="Admin")
                    {
                        admin admin=new admin();
                        admin.Show();
                        this.Close();
                    }
                    else if (dr[6].ToString()=="Staff")
                    {
                        // POS pos = new POS();
                        // pos.Show();
                        // this.Close();
                    }
                   // MessageBox.Show("Success");
                } else
                {
                    MessageBox.Show("Incorrect login or password");
                }
                Connect.con.Close();
            }
        }

        private void CreateAcc(object sender, RoutedEventArgs e)
        {
            Login wnd = new Login();
            this.Content = wnd;
        }
    }
}
