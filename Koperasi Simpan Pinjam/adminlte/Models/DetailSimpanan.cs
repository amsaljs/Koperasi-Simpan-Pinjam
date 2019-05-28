using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminlte.Models
{
    public class DetailSimpanan
    {
        public int no_simpan { get; set; }
        public Nullable<double> debet { get; set; }
        public Nullable<double> kredit { get; set; }
        public Nullable<double> saldo { get; set; }
        public string no_ang { get; set; }
    }
}