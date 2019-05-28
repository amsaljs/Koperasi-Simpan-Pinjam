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
    public class LoginController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
   
            return View();
        }

        [HttpPost]
        public ActionResult Index( AkunModel akun)
        {
            DataTable datatbl = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                
                sqlcon.Open();

                string query = "SELECT * FROM akun WHERE email = @email AND password= @password";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@email", akun.email);
                sqlDa.SelectCommand.Parameters.AddWithValue("@password", akun.password);

                sqlDa.Fill(datatbl);
            }

            if (datatbl.Rows.Count == 1)
            {
                Session["id"] = datatbl.Rows[0]["id_akun"];
                Session["nama"] = datatbl.Rows[0]["nama_akun"];

                return RedirectToAction("Dashboardv1", "Dashboard");
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["nama"] = null;

            return RedirectToAction("Index", "Login");
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
