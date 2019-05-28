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
    public class SimpananController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";


        [HttpGet]
        // GET: Simpanan
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From simpanan", sqlcon);
                sqlDa.Fill(dtbSimpan);
            }
            return View(dtbSimpan);
        }

        // GET: Simpanan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Simpanan/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Simpanan/Create
        [HttpPost]
        public ActionResult Create(SimpananModel simpan)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "INSERT INTO simpanan VALUES(@jns_simp)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@jns_simp", simpan.jns_simp);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Simpanan/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            SimpananModel simpan = new SimpananModel();
            DataTable dtbSimpan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM simpanan where kd_simp = @kd_simp";
                SqlDataAdapter sqlCmd = new SqlDataAdapter(query, sqlcon);
                sqlCmd.SelectCommand.Parameters.AddWithValue("@kd_simp", id);
                sqlCmd.Fill(dtbSimpan);
            }
            if (dtbSimpan.Rows.Count == 1)
            {
                simpan.kd_simp = Convert.ToInt32(dtbSimpan.Rows[0][0].ToString());
                simpan.jns_simp = dtbSimpan.Rows[0][1].ToString();

                return View(simpan);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Simpanan/Edit/5
        [HttpPost]
        public ActionResult Edit(SimpananModel simpan)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();

                string query = "UPDATE simpanan SET jns_simp=@jns_simp Where kd_simp = @kd_simp";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@kd_simp", simpan.kd_simp);
                sqlCmd.Parameters.AddWithValue("@jns_simp", simpan.jns_simp);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Simpanan/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                string query = "DELETE FROM simpanan Where kd_simp = @kd_simp";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Connection.Open();
                sqlCmd.Parameters.AddWithValue("@kd_simp", id);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: Simpanan/Delete/5
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
