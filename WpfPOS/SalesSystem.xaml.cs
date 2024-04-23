using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for SalesSystem.xaml
    /// </summary>
    public partial class SalesSystem : Page
    {
        public SalesSystem()
        {
            InitializeComponent();
            loadProducts();
            lItems.BeginningEdit += (s, ss) => ss.Cancel = true;
            sItems.BeginningEdit += (s, ss) => ss.Cancel = true;
        }

        private void loadProducts()
        {
            try
            {
                lItems.Items.Clear();
                Connect.con.Open();
                string all_query = "SELECT * FROM Inventory";
                SqlCommand cmd = new SqlCommand(all_query, Connect.con);

                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    string path = Directory.GetCurrentDirectory();
                    string folder_path = path.Substring(0, path.IndexOf("bin")) + dr["ProductImage"].ToString();

                    var productData = new Product
                    {
                        ID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = dr["Price"].ToString(),
                        Picture = folder_path,
                        Type = dr["Type"].ToString()
                    };

                    lItems.Items.Add(productData);
                }


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Connect.con.Close();
            }
        }

        private void lItems_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                Product product = lItems.SelectedItem as Product;

                if (product != null)
                {
                    var productData = new Product
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,
                    };

                    sItems.Items.Add(productData);

                    int subTot = Int32.Parse(subTotal.Text) + Int32.Parse(productData.Price);
                    double tax = subTot * 0.02;
                    double total = subTot + tax;

                    subTotal.Text = Convert.ToString(subTot);
                    taxInput.Text = Convert.ToString(tax);
                    grandTotal.Text = Convert.ToString(total);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void removeItem_click(object sender, RoutedEventArgs e)
        {
            Product product = sItems.SelectedItem as Product;

            var productData = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price,
            };

            int subTot = Int32.Parse(subTotal.Text) - Int32.Parse(productData.Price);
            double tax = subTot * 0.02;
            double total = subTot + tax;

            subTotal.Text = Convert.ToString(subTot);
            taxInput.Text = Convert.ToString(tax);
            grandTotal.Text = Convert.ToString(total);

            sItems.Items.RemoveAt(sItems.SelectedIndex);
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            double paidAmount = Convert.ToDouble(Cash.Text),
                   change = Convert.ToDouble(Change.Text);

            if (change < 0)
            {
                MessageBox.Show("Paid amount should be equal or more than grand total", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Success. Please enter new order", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                sItems.Items.Clear();
                subTotal.Text = Convert.ToString(0);
                taxInput.Text = Convert.ToString(0);
                Cash.Text = Convert.ToString(0);
                Change.Text = Convert.ToString(0);
            }
        }

        private void Cash_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double paidAmount = Convert.ToDouble(Cash.Text),
                                   toPay = Convert.ToDouble(grandTotal.Text),
                                   change = paidAmount - toPay;

                Change.Text = Convert.ToString(change);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Change.Text = "";
            }

        }
    }
}
