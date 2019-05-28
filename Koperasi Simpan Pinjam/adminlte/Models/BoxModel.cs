using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminlte.Models
{
    public class BoxModel
    {
        public int anggota { get; set; }
        public double kredit { get; set; }
        public double debet { get; set; }
        public double saldo { get; set; }
    }
}