using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;


namespace IMS.Controllers
{
    public class IssuesController : Controller
    {

        StockManagementEntities data = new StockManagementEntities();
        // GET: Issues
        public ActionResult Index()
        {
            var model = data.getAllIssues();
            return View(model);
        }

        // GET: Issues/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Issues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
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

        // GET: Issues/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Issues/Edit/5
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

        // GET: Issues/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Issues/Delete/5
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
