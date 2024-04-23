using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfPOS
{
    internal class Connect
    {  
        public static SqlConnection con;
        public void setConnection()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shmik\Desktop\WpfPOS\WpfPOS\WpfPos.mdf;Integrated Security=True");
        }

    }
}
