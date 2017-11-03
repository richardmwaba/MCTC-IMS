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
    public class EmployeeDetailsController : Controller
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: EmployeeDetails
        public ActionResult Index()
        {
            var employee_Details = db.Employee_Details.Include(e => e.user);
            return View(employee_Details.ToList());
        }

        // GET: EmployeeDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Details employee_Details = db.Employee_Details.Find(id);
            if (employee_Details == null)
            {
                return HttpNotFound();
            }
            return View(employee_Details);
        }

        // GET: EmployeeDetails/Create
        public ActionResult Create()
        {
            ViewBag.mine_number = new SelectList(db.users, "mine_number", "role_ID");
            return View();
        }

        // POST: EmployeeDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,surname,firstname,other_names,mine_number,site,department,position,job_title")] Employee_Details employee_Details)
        {
            if (ModelState.IsValid)
            {
                db.Employee_Details.Add(employee_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mine_number = new SelectList(db.users, "mine_number", "role_ID", employee_Details.mine_number);
            return View(employee_Details);
        }

        // GET: EmployeeDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Details employee_Details = db.Employee_Details.Find(id);
            if (employee_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.mine_number = new SelectList(db.users, "mine_number", "role_ID", employee_Details.mine_number);
            return View(employee_Details);
        }

        // POST: EmployeeDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,surname,firstname,other_names,mine_number,site,department,position,job_title")] Employee_Details employee_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mine_number = new SelectList(db.users, "mine_number", "role_ID", employee_Details.mine_number);
            return View(employee_Details);
        }

        // GET: EmployeeDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Details employee_Details = db.Employee_Details.Find(id);
            if (employee_Details == null)
            {
                return HttpNotFound();
            }
            return View(employee_Details);
        }

        // POST: EmployeeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee_Details employee_Details = db.Employee_Details.Find(id);
            db.Employee_Details.Remove(employee_Details);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
