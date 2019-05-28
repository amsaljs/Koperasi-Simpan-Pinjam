using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using adminlte.Models;
using Newtonsoft.Json;

namespace adminlte.Controllers
{
    public class DashboardController : Controller
    {
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboardv1()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataTable ListHeader = new DataTable();
            DataTable ListPinjam = new DataTable();
            DataTable ListSimpan = new DataTable();
            DataTable ListAnggota = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From view_header", sqlcon);
                sqlDa.Fill(ListHeader);

                SqlDataAdapter sqlDb = new SqlDataAdapter("SELECT * From view_chartPinjaman", sqlcon);
                sqlDb.Fill(ListPinjam);

                SqlDataAdapter sqlDc = new SqlDataAdapter("SELECT * From view_chartSimpanan", sqlcon);
                sqlDc.Fill(ListSimpan);

                SqlDataAdapter sqlDd = new SqlDataAdapter("SELECT * From view_anggota", sqlcon);
                sqlDd.Fill(ListAnggota);

                Chart cartModel = new Chart();
                cartModel.dataTable1 = ListHeader;
                cartModel.dataTable2 = ListPinjam;
                cartModel.dataTable3 = ListSimpan;
                cartModel.dataTable4 = ListAnggota;
                ViewBag.datapoint = JsonConvert.SerializeObject(cartModel.dataTable4, _jsonSetting);

                //             chart.anggota = Convert.ToInt32(ListHeader.Rows[0][0].ToString());
                //           chart.kredit = Convert.ToDouble(ListHeader.Rows[0][1].ToString());
                //          chart.debet = Convert.ToDouble(ListHeader.Rows[0][2].ToString());
                //        chart.saldo = Convert.ToDouble(ListHeader.Rows[0][3].ToString());

                //      chart.jumlah = Convert.ToInt32(ListAnggota.Rows[0][0].ToString());
                //    chart.pekerjaan = ListAnggota.Rows[0][1].ToString();

                //  chart.tglsimpan = ListSimpan.Rows[0][0].ToString();
                //chart.saldoSimpan = Convert.ToDouble(ListSimpan.Rows[0][1].ToString());

                //               chart.tglpinj = ListPinjam.Rows[0][0].ToString();
                //             chart.saldoPinjam = Convert.ToDouble(ListPinjam.Rows[0][1].ToString());
                //
                //           ViewBag.datapoint = JsonConvert.SerializeObject(ListAnggota, _jsonSetting);
                //         ViewBag.datapoint1 = JsonConvert.SerializeObject(ListSimpan, _jsonSetting);
                //       ViewBag.datapoint2 = JsonConvert.SerializeObject(ListPinjam, _jsonSetting);


                return View(cartModel);
            }

        }

        public ActionResult Dashboardv2()
        {
            return View();
        }
    }
}