using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminlte.Models
{
    public class AkunModel
    {
        public int id_akun { get; set; }
        public string nama_akun { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}