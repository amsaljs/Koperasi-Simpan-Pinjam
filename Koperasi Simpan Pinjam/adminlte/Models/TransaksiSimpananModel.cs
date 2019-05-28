using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminlte.Models
{
    public class TransaksiSimpananModel
    {
        public int id_tsimpan { get; set; }

        public string no_simpan { get; set; }
        public DateTime tglsimpan { get; set; }
        public string jenis { get; set; }
        public double saldo { get; set; }
        public string no_ang { get; set; }
    }
}