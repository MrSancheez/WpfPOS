using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPOS
{
    /// <summary>
    /// This class describes products in the system
    /// </summary>
    internal class Product
    {
        public int ID { get; set; }

        public string ProductName { get; set; }

        public string Type { get; set; }
        public string Price { get; set; }

        public string Picture { get; set; }
        public string PictureSource { get; set; }
    }
}
