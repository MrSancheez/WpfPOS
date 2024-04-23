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
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WpfPOS
{
    /// <summary>
    /// Interaction logic for POS.xaml
    /// </summary>
    public partial class POS : Page
    {
        public POS()
        {
            InitializeComponent();
            Connect connect = new Connect();
            connect.setConnection();
           
            Random rand = new Random();
            product_ID.Text = "" + rand.Next(10000, 50000);
            loadData();
        }

        private void browse_btn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    fileName.Text = dialog.FileName;
                    product_image.Source = new BitmapImage(new Uri(fileName.Text, UriKind.Absolute));

                    string source = fileName.Text;
                    FileInfo info = new FileInfo(source);
                    string destination = @"C:\Users\shmik\Desktop\WpfPOS\WpfPOS\productimages\" + System.IO.Path.GetFileName(source);

                    info.CopyTo(destination);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void add_btn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                string ProductID = product_ID.Text;
                string ProductName = product_name.Text;
                string Price = price.Text;
                string Type = type.Text;

                if (string.IsNullOrEmpty(product_ID.Text) || string.IsNullOrEmpty(product_name.Text) || string.IsNullOrEmpty(price.Text) || string.IsNullOrEmpty(type.Text))
                {
                    MessageBox.Show("Please fill all the required fields");
                }
                else
                {
                    String query = "INSERT INTO Inventory VALUES('" + ProductID + "', '" + ProductName + "', '" + Type + "', '" + Price + "', '/productimages/" + System.IO.Path.GetFileName(fileName.Text) + "' )";
                    SqlCommand cmd = new SqlCommand(query, Connect.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("POS saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.con.Close();
                loadData();
            }
        }
        private void delete_btn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                String query = "DELETE FROM Inventory WHERE ProductID = '" + product_ID.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, Connect.con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("record was successfully deleted");
                clear();
                loadData();

                

            }
            catch (SqlException ex)
            {
                MessageBox.Show("" +ex);
            }
            finally
            {
                Connect.con.Close();
                loadData();
            }
        }
        public void clear()
        {
            product_ID.Text = "";
            product_name.Text = "";
            type.Text = "";
            price.Text = "";
        }
        private void update_btn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                string ProductID = product_ID.Text;
                string ProductName = product_name.Text;
                string Price = price.Text;
                string Type = type.Text;
                string picture = fileName.Text;

                if (string.IsNullOrEmpty(product_ID.Text) || string.IsNullOrEmpty(product_name.Text) || string.IsNullOrEmpty(price.Text) || string.IsNullOrEmpty(type.Text))
                {
                    MessageBox.Show("Please fill all the required fields");
                }
                else
                {
                    String query = $"UPDATE Inventory SET ProductName='{ProductName}', Type='{Type}', Price='{Price}', ProductImage='{picture}' WHERE ProductID={ProductID}";
                    SqlCommand cmd = new SqlCommand(query, Connect.con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("POS saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.con.Close();
            }
        }

        public void loadData()
        {
            try
            {
                String query = "SELECT * FROM Inventory";
                SqlCommand cmd = new SqlCommand(query, Connect.con);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                lProducts.ItemsSource = dt.DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void search_btn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect.con.Open();
                String query = "SELECT * FROM Inventory WHERE ProductID ='"+product_ID.Text+"'";
                SqlCommand cmd = new SqlCommand(query, Connect.con);
                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    product_name.Text=dr["ProductName"].ToString();
                    type.Text = dr["Type"].ToString();
                    price.Text = dr["Price"].ToString();
                    fileName.Text = dr["ProductImage"].ToString();

                    if (dr["ProductImage"].ToString() != "")
                    {
                        string path = Directory.GetCurrentDirectory();
                        string image_source = path.Substring(0, path.IndexOf("bin")) + dr["ProductImage"].ToString();
                        product_image.Source = new BitmapImage(new Uri(image_source));
                    }
                }
                else
                {
                    MessageBox.Show("Product was not found");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.con.Close();
            }
        }
    }

  
}
  

