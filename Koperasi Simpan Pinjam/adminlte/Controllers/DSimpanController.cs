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
    public class DSimpanController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-Q01413R; Initial Catalog = Koperasi; Integrated Security = True";
        // GET: DSimpan
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
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * From view_simpanan", sqlcon);
                sqlDa.Fill(dtbSimpan);
            }
            return View(dtbSimpan);
        }

        // GET: DSimpan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DSimpan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DSimpan/Create
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

        // GET: DSimpan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DSimpan/Edit/5
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

        // GET: DSimpan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DSimpan/Delete/5
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
