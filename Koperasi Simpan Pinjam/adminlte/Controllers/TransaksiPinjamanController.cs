using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using adminlte.Models;
using System.Data.Entity.Validation;
using pinjamanLibrary;

namespace adminlte.Controllers
{
    public class TransaksiPinjamanController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";

        [HttpGet]
        // GET: TransaksiPinjaman
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From trans_pinjaman", sqlcon);

                sqlDa.Fill(dtbSimpan);
            }
            return View(dtbSimpan);

        }

        // GET: TransaksiPinjaman/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransaksiPinjaman/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            KoperasiEntities2 db = new KoperasiEntities2();
            List<anggota> list = db.anggotas.ToList();
            List<pinjaman> list1 = db.pinjamen.ToList();
            ViewBag.anggotaList = new SelectList(list, "no_anggota", "nama_ang");
            ViewBag.pinjamanList = new SelectList(list1, "kd_pinj", "jns_pinj");

            return View();
        }

        // POST: TransaksiPinjaman/Create
        [HttpPost]
        public ActionResult Create(TransaksiPinjamanModel pinjam)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                DataTable dtbSimpan = new DataTable();
                sqlcon.Open();
                KoperasiEntities2 db = new KoperasiEntities2();
                List<anggota> list = db.anggotas.ToList();
                List<pinjaman> list1 = db.pinjamen.ToList();
                ViewBag.anggotaList = new SelectList(list, "no_anggota", "nama_ang");
                ViewBag.pinjamanList = new SelectList(list1, "kd_pinj", "jns_pinj");

                string query = "SELECT bunga FROM pinjaman where kd_pinj = @kd_pinj";
                SqlDataAdapter sqlCmd = new SqlDataAdapter(query, sqlcon);
                sqlCmd.SelectCommand.Parameters.AddWithValue("@kd_pinj", pinjam.kd_pinj);
                sqlCmd.Fill(dtbSimpan);


                trans_pinjaman tpinjam = new trans_pinjaman();
                Peminjaman objpinjam = new Peminjaman();
                var bunga = Convert.ToDouble(dtbSimpan.Rows[0][0].ToString());
                var jumlah = pinjam.jlh;

                var bungaTot = objpinjam.totalPeminjaman(bunga , jumlah);
                var total = bungaTot + jumlah;
                var angsuran = total / pinjam.lama;
                tpinjam.no_ang = pinjam.no_ang;
                tpinjam.jlh = pinjam.jlh;
                tpinjam.keterangan = pinjam.keterangan;
                tpinjam.lama = pinjam.lama;
                tpinjam.bunga= Convert.ToDouble(dtbSimpan.Rows[0][0].ToString());
                tpinjam.status = "Belum Lunas";
                tpinjam.kd_pinj = pinjam.kd_pinj;
                tpinjam.tglpinj = pinjam.tglpinj;
                tpinjam.total = total;
                tpinjam.angsuran = angsuran;

                db.trans_pinjaman.Add(tpinjam);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult UbahStatus(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query2 = "UPDATE trans_pinjaman SET status=@status Where no_pinj = @no_pinj";
                SqlCommand sqlCmd2 = new SqlCommand(query2, sqlcon);
                sqlCmd2.Parameters.AddWithValue("@no_pinj", id);
                sqlCmd2.Parameters.AddWithValue("@status", "Lunas");
                sqlCmd2.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: TransaksiPinjaman/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            KoperasiEntities2 db = new KoperasiEntities2();
            List<anggota> list = db.anggotas.ToList();
            List<pinjaman> list1 = db.pinjamen.ToList();
            ViewBag.anggotaList = new SelectList(list, "no_anggota", "nama_ang");
            ViewBag.pinjamanList = new SelectList(list1, "kd_pinj", "jns_pinj");

            TransaksiPinjamanModel Tpinjam = new TransaksiPinjamanModel();
            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM trans_pinjaman where no_pinj = @no_pinj";
                SqlDataAdapter sqlCmd = new SqlDataAdapter(query, sqlcon);
                sqlCmd.SelectCommand.Parameters.AddWithValue("@no_pinj", id);
                sqlCmd.Fill(dtbSimpan);
            }
            if (dtbSimpan.Rows.Count == 1)
            {
                Tpinjam.no_pinj = Convert.ToInt32(dtbSimpan.Rows[0][0].ToString());
                Tpinjam.tglpinj = Convert.ToDateTime(dtbSimpan.Rows[0][1].ToString());
                Tpinjam.bunga = Convert.ToDouble(dtbSimpan.Rows[0][2].ToString());
                Tpinjam.jlh = Convert.ToDouble(dtbSimpan.Rows[0][3].ToString());
                Tpinjam.total = Convert.ToDouble(dtbSimpan.Rows[0][4].ToString());
                Tpinjam.angsuran = Convert.ToDouble(dtbSimpan.Rows[0][5].ToString());
                Tpinjam.status = dtbSimpan.Rows[0][6].ToString();
                Tpinjam.kd_pinj = dtbSimpan.Rows[0][7].ToString();
                Tpinjam.no_ang = dtbSimpan.Rows[0][8].ToString();
                Tpinjam.keterangan= dtbSimpan.Rows[0][9].ToString();
                Tpinjam.lama = Convert.ToDouble(dtbSimpan.Rows[0][10].ToString());

                return View(Tpinjam);
            }
            else
                return RedirectToAction("Index");


        }

        // POST: TransaksiPinjaman/Edit/5
        [HttpPost]
        public ActionResult Edit(TransaksiPinjamanModel pinjam)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            KoperasiEntities2 db = new KoperasiEntities2();
            List<anggota> list = db.anggotas.ToList();
            List<pinjaman> list1 = db.pinjamen.ToList();
            ViewBag.anggotaList = new SelectList(list, "no_anggota", "nama_ang");
            ViewBag.pinjamanList = new SelectList(list1, "kd_pinj", "jns_pinj");

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                DataTable dtbSimpan = new DataTable();
                sqlcon.Open();

                string query2 = "SELECT bunga FROM pinjaman where kd_pinj = @kd_pinj";
                SqlDataAdapter sqlCmd2 = new SqlDataAdapter(query2, sqlcon);
                sqlCmd2.SelectCommand.Parameters.AddWithValue("@kd_pinj", pinjam.kd_pinj);
                sqlCmd2.Fill(dtbSimpan);

                var total = (Convert.ToDouble(dtbSimpan.Rows[0][0].ToString()) * pinjam.jlh) / 100;
                var angsuran = total / pinjam.lama;

                string query = "UPDATE trans_pinjaman SET tglpinj=@tglpinj, bunga=@bunga, jlh=@jlh, total=@total, angsuran=@angsuran, status=@status, kd_pinj=@kd_pinj, no_ang=@no_ang, keterangan=@keterangan, lama=@lama Where no_pinj = @no_pinj";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                
                sqlCmd.Parameters.AddWithValue("@no_pinj", pinjam.no_pinj);
                sqlCmd.Parameters.AddWithValue("@tglpinj", pinjam.tglpinj);
                sqlCmd.Parameters.AddWithValue("@bunga", Convert.ToDouble(dtbSimpan.Rows[0][0].ToString()));
                sqlCmd.Parameters.AddWithValue("@jlh", pinjam.jlh);
                sqlCmd.Parameters.AddWithValue("@total", total);
                sqlCmd.Parameters.AddWithValue("@angsuran", angsuran);
                sqlCmd.Parameters.AddWithValue("@status", "Belum Lunas");
                sqlCmd.Parameters.AddWithValue("@kd_pinj", pinjam.kd_pinj);
                sqlCmd.Parameters.AddWithValue("@no_ang", pinjam.no_ang);
                sqlCmd.Parameters.AddWithValue("@keterangan", pinjam.keterangan);
                sqlCmd.Parameters.AddWithValue("@lama", pinjam.lama);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: TransaksiPinjaman/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "DELETE FROM trans_pinjaman Where no_pinj = @no_pinj";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@no_pinj", id);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: TransaksiPinjaman/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
