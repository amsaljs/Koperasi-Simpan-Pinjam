using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using adminlte.Models;
using randomNumberLibrary;

namespace adminlte.Controllers
{
    public class AnggotaController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";

        [HttpGet]
        // GET: Anggota
        public ActionResult Index()
        {
            if(Session["id"]== null)
            {
                return RedirectToAction("Index", "Login");
            }
            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From anggota", sqlcon);
                sqlDa.Fill(dtbSimpan);
            }
            return View(dtbSimpan);
        }

        // GET: Anggota/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Anggota/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Anggota/Create
        [HttpPost]
        public ActionResult Create(AnggotaModel anggota)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO anggota VALUES(@no_anggota, @nama_ang, @alamat, @kota, @no_telp, @pekerjaan, @no_simp)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                RandomNumber objrandom = new RandomNumber();
                var id = objrandom.generateNumber("KSP");
                var kds = objrandom.generateNumber("SPN");
                sqlCmd.Parameters.AddWithValue("@no_anggota", id);
                sqlCmd.Parameters.AddWithValue("@nama_ang", anggota.nama_ang);
                sqlCmd.Parameters.AddWithValue("@alamat", anggota.alamat);
                sqlCmd.Parameters.AddWithValue("@kota", anggota.kota);
                sqlCmd.Parameters.AddWithValue("@no_telp", anggota.no_telp);
                sqlCmd.Parameters.AddWithValue("@pekerjaan", anggota.pekerjaan);
                sqlCmd.Parameters.AddWithValue("@no_simp", kds);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Anggota/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AnggotaModel anggota = new AnggotaModel();
            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM anggota where no_anggota = @no_anggota";
                SqlDataAdapter sqlCmd = new SqlDataAdapter(query, sqlcon);
                sqlCmd.SelectCommand.Parameters.AddWithValue("@no_anggota", id);
                sqlCmd.Fill(dtbSimpan);
            }
            if (dtbSimpan.Rows.Count == 1)
            {
                anggota.no_anggota = dtbSimpan.Rows[0][0].ToString();
                anggota.nama_ang = dtbSimpan.Rows[0][1].ToString();
                anggota.alamat = dtbSimpan.Rows[0][2].ToString();
                anggota.kota = dtbSimpan.Rows[0][3].ToString();
                anggota.no_telp = dtbSimpan.Rows[0][4].ToString();
                anggota.pekerjaan = dtbSimpan.Rows[0][5].ToString();
                anggota.no_simp = dtbSimpan.Rows[0][6].ToString();

                return View(anggota);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Anggota/Edit/5
        [HttpPost]
        public ActionResult Edit(AnggotaModel anggota)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE anggota SET no_anggota=@no_anggota, no_simp=@no_simp, nama_ang=@nama_ang, alamat=@alamat, kota=@kota, no_telp=@no_telp, pekerjaan=@pekerjaan Where no_anggota = @no_anggota";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@no_anggota", anggota.no_anggota);
                sqlCmd.Parameters.AddWithValue("@nama_ang", anggota.nama_ang);
                sqlCmd.Parameters.AddWithValue("@alamat", anggota.alamat);
                sqlCmd.Parameters.AddWithValue("@kota", anggota.kota);
                sqlCmd.Parameters.AddWithValue("@no_telp", anggota.no_telp);
                sqlCmd.Parameters.AddWithValue("@pekerjaan", anggota.pekerjaan);
                sqlCmd.Parameters.AddWithValue("@no_simp", anggota.no_simp);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Anggota/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "DELETE FROM anggota Where no_anggota = @no_anggota";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@no_anggota", id);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: Anggota/Delete/5
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
