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
    public class ShelfCompartmentController : Controller
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: ShelfCompartment
        public ActionResult Index()
        {
            var shelf_Compartment = db.Shelf_Compartment.Include(s => s.Shelves_Table);
            return View(shelf_Compartment.ToList());
        }

        // GET: ShelfCompartment/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelf_Compartment shelf_Compartment = db.Shelf_Compartment.Find(id);
            if (shelf_Compartment == null)
            {
                return HttpNotFound();
            }
            return View(shelf_Compartment);
        }

        // GET: ShelfCompartment/Create
        public ActionResult Create()
        {
            ViewBag.shelf_ID = new SelectList(db.Shelves_Table, "shelf_ID", "shelf_allocation");
            return View();
        }

        // POST: ShelfCompartment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,compartment_ID,shelf_ID")] Shelf_Compartment shelf_Compartment)
        {
            if (ModelState.IsValid)
            {
                db.Shelf_Compartment.Add(shelf_Compartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.shelf_ID = new SelectList(db.Shelves_Table, "shelf_ID", "shelf_allocation", shelf_Compartment.shelf_ID);
            return View(shelf_Compartment);
        }

        // GET: ShelfCompartment/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelf_Compartment shelf_Compartment = db.Shelf_Compartment.Find(id);
            if (shelf_Compartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.shelf_ID = new SelectList(db.Shelves_Table, "shelf_ID", "shelf_allocation", shelf_Compartment.shelf_ID);
            return View(shelf_Compartment);
        }

        // POST: ShelfCompartment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,compartment_ID,shelf_ID")] Shelf_Compartment shelf_Compartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shelf_Compartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.shelf_ID = new SelectList(db.Shelves_Table, "shelf_ID", "shelf_allocation", shelf_Compartment.shelf_ID);
            return View(shelf_Compartment);
        }

        // GET: ShelfCompartment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelf_Compartment shelf_Compartment = db.Shelf_Compartment.Find(id);
            if (shelf_Compartment == null)
            {
                return HttpNotFound();
            }
            return View(shelf_Compartment);
        }

        // POST: ShelfCompartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Shelf_Compartment shelf_Compartment = db.Shelf_Compartment.Find(id);
            db.Shelf_Compartment.Remove(shelf_Compartment);
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
