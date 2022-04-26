using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnKTools
{
    class Transaksi
    {
        public int id { get; set; }
        public string kode { get; set; }
        public double total { get; set; }
        public string currency { get; set; }
        public double buy { get; set; }
        public double sell { get; set; }
        public DateTime date { get; set; }
        public double profit { get; set; }
    }
}
