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

namespace WpfPOS
{
    /// <summary>
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Page
    {
        public Staff()
        {
            InitializeComponent();
            Connect connect = new Connect();
            connect.setConnection();
        }

        private void emp_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                String query = "SELECT * FROM Users WHERE UserName='" + user_name.Text + "'";
                SqlCommand q_cmd = new SqlCommand(query, Connect.con);
                SqlDataReader dr = q_cmd.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("Username already exists");
                }
                else
                {
                    dr.Close();
                    String add_query = "INSERT INTO Users(UserName,EmployeeName,Qualification,Password) VALUES('" + user_name.Text + "', '" + emp_Name.Text + "','" + emp_Qu.Text +
                        "','" + emp_Pass.Password + "')";
                    SqlCommand cmd = new SqlCommand(add_query, Connect.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Saved");
                }

                Connect.con.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("" + ex);
            }
            finally
            {
                Connect.con.Close();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                String query = "SELECT * FROM Users WHERE UserName='" + user_name.Text + "'";
                SqlCommand q_cmd = new SqlCommand(query, Connect.con);
                SqlDataReader dr = q_cmd.ExecuteReader();

                if (dr.Read())
                {
                    emp_Name.Text = dr[1].ToString();
                    emp_Qu.Text = dr[2].ToString();
                    emp_Pass.Password = dr[3].ToString();
                }
                else
                {
                    MessageBox.Show("UserName not found");
                }

                Connect.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("" + ex);

            }
            finally
            {
                Connect.con.Close();
            }
        }

        private void emp_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                String d_query = "DELETE FROM Users WHERE UserName='" + user_name.Text + "'";
                SqlCommand d_cmd = new SqlCommand(d_query, Connect.con);
                d_cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                clear();
                Connect.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("" + ex);

            }
            finally
            {
                Connect.con.Close();
            }
        }
            public void clear()
        {
            user_name.Clear();
            emp_Name.Clear();
            emp_Pass.Clear();
            emp_Qu.Clear();
        }

        private void emp_Update_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                String u_query = "UPDATE Users SET EmployeeName='" + emp_Name.Text + "', Qualification='" + emp_Qu.Text + "', Password='" + emp_Pass.Password + "' WHERE UserName='" + user_name.Text + "'";
                SqlCommand u_cmd = new SqlCommand(u_query, Connect.con);
                u_cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated");
                Connect.con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("" + ex);

            }
            finally
            {
                Connect.con.Close();
            }
        }
    }
}
