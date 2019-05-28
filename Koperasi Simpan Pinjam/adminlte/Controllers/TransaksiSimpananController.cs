using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using adminlte.Models;
using System.Data.Entity.Validation;

namespace adminlte.Controllers
{
    public class TransaksiSimpananController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";

        [HttpGet]
        // GET: TransaksiSimpanan
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
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From trans_simpanan", sqlcon);
                sqlDa.Fill(dtbSimpan);
            }
            return View(dtbSimpan);
        }

        // GET: TransaksiSimpanan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult DSimpan()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From view_simpanan", sqlcon);
                sqlDa.Fill(dtbSimpan);
            }
            return View(dtbSimpan);
        }

        // GET: TransaksiSimpanan/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            KoperasiEntities2 db = new KoperasiEntities2();
            List<anggota> list2 = db.anggotas.ToList();
            List<simpanan> list1 = db.simpanans.ToList();
            ViewBag.anggotaList2 = new SelectList(list2, "no_anggota", "nama_ang");
            ViewBag.simpananList = new SelectList(list1, "kd_simp", "jns_simp");

            return View();
        }

        // POST: TransaksiSimpanan/Create
        [HttpPost]
        public ActionResult Create(TransaksiSimpananModel simpan)
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
                AnggotaModel anggota = new AnggotaModel();
                List<anggota> list = db.anggotas.ToList();
                List<simpanan> list1 = db.simpanans.ToList();
                ViewBag.anggotaList = new SelectList(list, "no_anggota", "nama_ang");
                ViewBag.anggotaList1 = new SelectList(list, "no_simp", "nama_ang");
                ViewBag.simpananList = new SelectList(list1, "kd_simp", "jns_simp");

                string query = "SELECT no_simp FROM anggota where no_anggota = @no_anggota";
                SqlDataAdapter sqlCmd = new SqlDataAdapter(query, sqlcon);
                sqlCmd.SelectCommand.Parameters.AddWithValue("@no_anggota", simpan.no_ang);
                sqlCmd.Fill(dtbSimpan);


                trans_simpanan tsimpan = new trans_simpanan();
                tsimpan.no_ang = simpan.no_ang;
                tsimpan.tglsimpan = simpan.tglsimpan;
                tsimpan.jenis = simpan.jenis;
                tsimpan.saldo = simpan.saldo;
                tsimpan.no_simpan = dtbSimpan.Rows[0][0].ToString();
                db.trans_simpanan.Add(tsimpan);
                db.SaveChanges();        
                

            }

            return RedirectToAction("Index");
        }

        // GET: TransaksiSimpanan/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            TransaksiSimpananModel Tsimpan = new TransaksiSimpananModel();
            DataTable dtbSimpan = new DataTable();

            KoperasiEntities2 db = new KoperasiEntities2();
            List<anggota> list2 = db.anggotas.ToList();
            List<simpanan> list1 = db.simpanans.ToList();
            ViewBag.anggotaList2 = new SelectList(list2, "no_anggota", "nama_ang");
            ViewBag.simpananList = new SelectList(list1, "kd_simp", "jns_simp");

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM trans_simpanan where id_tsimpan = @id_tsimpan";
                SqlDataAdapter sqlCmd = new SqlDataAdapter(query, sqlcon);
                sqlCmd.SelectCommand.Parameters.AddWithValue("@id_tsimpan", id);
                sqlCmd.Fill(dtbSimpan);
            }
            if (dtbSimpan.Rows.Count == 1)
            {
                Tsimpan.id_tsimpan = Convert.ToInt32(dtbSimpan.Rows[0][0].ToString());
                Tsimpan.no_simpan = dtbSimpan.Rows[0][1].ToString();
                Tsimpan.tglsimpan = Convert.ToDateTime(dtbSimpan.Rows[0][2].ToString());
                Tsimpan.jenis = dtbSimpan.Rows[0][3].ToString();
                Tsimpan.saldo = Convert.ToDouble(dtbSimpan.Rows[0][4].ToString());
                Tsimpan.no_ang = dtbSimpan.Rows[0][5].ToString();

                return View(Tsimpan);
            }
            else
                return RedirectToAction("Index");
            
        }

        // POST: TransaksiSimpanan/Edit/5
        [HttpPost]
        public ActionResult Edit(TransaksiSimpananModel simpan)
        {
            KoperasiEntities2 db = new KoperasiEntities2();
            List<anggota> list2 = db.anggotas.ToList();
            List<simpanan> list1 = db.simpanans.ToList();
            ViewBag.anggotaList2 = new SelectList(list2, "no_anggota", "nama_ang");
            ViewBag.simpananList = new SelectList(list1, "kd_simp", "jns_simp");

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "UPDATE trans_simpanan SET no_simpan=@no_simpan, no_ang = @no_ang, tglsimpan=@tglsimpan, jenis=@jenis, saldo=@saldo Where id_tsimpan = @id_tsimpan";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@id_tsimpan", simpan.id_tsimpan);
                sqlCmd.Parameters.AddWithValue("@no_simpan", simpan.no_simpan);
                sqlCmd.Parameters.AddWithValue("@tglsimpan", simpan.tglsimpan);
                sqlCmd.Parameters.AddWithValue("@jenis", simpan.jenis);
                sqlCmd.Parameters.AddWithValue("@saldo", simpan.saldo);
                sqlCmd.Parameters.AddWithValue("@no_ang", simpan.no_ang);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: TransaksiSimpanan/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "DELETE FROM trans_simpanan Where id_tsimpan = @id_tsimpan";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@id_tsimpan", id);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");

        }

        // POST: TransaksiSimpanan/Delete/5
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
