using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using adminlte.Models;

namespace adminlte.Controllers
{
    public class PinjamanController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";


        [HttpGet]
        // GET: Pinjaman
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DataTable dtbPinjam = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From pinjaman", sqlcon);
                sqlDa.Fill(dtbPinjam);
            }
            return View(dtbPinjam);
        }

        // GET: Pinjaman/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pinjaman/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Pinjaman/Create
        [HttpPost]
        public ActionResult Create(PinjamanModel pinjam)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "INSERT INTO pinjaman VALUES(@jns_pinj, @max_pinj, @max_ang, @bunga)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@jns_pinj", pinjam.jns_pinj);
                sqlCmd.Parameters.AddWithValue("@max_pinj", pinjam.max_pinj);
                sqlCmd.Parameters.AddWithValue("@max_ang", pinjam.max_ang);
                sqlCmd.Parameters.AddWithValue("@bunga", pinjam.bunga);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Pinjaman/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            PinjamanModel pinjam = new PinjamanModel();
            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM pinjaman where kd_pinj = @kd_pinj";
                SqlDataAdapter sqlCmd = new SqlDataAdapter(query, sqlcon);
                sqlCmd.SelectCommand.Parameters.AddWithValue("@kd_pinj", id);
                sqlCmd.Fill(dtbSimpan);
            }
            if (dtbSimpan.Rows.Count == 1)
            {
                pinjam.kd_pinj = dtbSimpan.Rows[0][0].ToString();
                pinjam.jns_pinj = dtbSimpan.Rows[0][1].ToString();
                pinjam.max_pinj = Convert.ToDouble(dtbSimpan.Rows[0][2].ToString());
                pinjam.max_ang = Convert.ToDouble(dtbSimpan.Rows[0][3].ToString());
                pinjam.bunga = Convert.ToDouble(dtbSimpan.Rows[0][4].ToString());

                return View(pinjam);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Pinjaman/Edit/5
        [HttpPost]
        public ActionResult Edit(PinjamanModel pinjam)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE pinjaman SET jns_pinj=@jns_pinj, max_pinj=@max_pinj, max_ang=@max_ang, bunga=@bunga Where kd_pinj = @kd_pinj";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@kd_pinj", pinjam.kd_pinj);
                sqlCmd.Parameters.AddWithValue("@jns_pinj", pinjam.jns_pinj);
                sqlCmd.Parameters.AddWithValue("@max_pinj", pinjam.max_pinj);
                sqlCmd.Parameters.AddWithValue("@max_ang", pinjam.max_ang);
                sqlCmd.Parameters.AddWithValue("@bunga", pinjam.bunga);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Pinjaman/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "DELETE FROM pinjaman Where kd_pinj = @kd_pinj";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@kd_pinj", id);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: Pinjaman/Delete/5
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
