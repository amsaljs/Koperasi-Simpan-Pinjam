using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminlte.Models
{
    public class TransaksiPinjamanModel
    {
        public int no_pinj { get; set; }
        public Nullable<DateTime> tglpinj { get; set; }
        public Nullable<double> bunga { get; set; }
        public double jlh { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<double> angsuran { get; set; }
        public string status { get; set; }
        public string kd_pinj { get; set; }
        public string no_ang { get; set; }
        public string keterangan { get; set; }
        public Nullable<double> lama { get; set; }
    }
}