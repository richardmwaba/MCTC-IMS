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
    public class MovementsController : Controller
    {
        private StockManagementEntities db = new StockManagementEntities();

        //// GET: Movements
        //public ActionResult Index()
        //{
        //    var movements = db.Movements.Include(m => m.Employee_Details).Include(m => m.Employee_Details1).Include(m => m.Return_Status1).Include(m => m.Request_Details).Include(m => m.Stock_Details).Include(m => m.Type_of_Transaction);
        //    return View(movements.ToList());
        //}

        // GET: Movements/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return HttpNotFound();
            }
            return View(movement);
        }

        // GET: All Reversals
        public ActionResult Reversals()
        {
            var model = db.getMovements("reversal");
            return View(model);
        }

        //GET: All receipts
        public ActionResult Receipts()
        {
            var model = db.getMovements("receipt");
            return View(model);
        }

        // GET: Movements/Create
        public ActionResult Create()
        {
            ViewBag.received_by = new SelectList(db.Employee_Details, "mine_number", "surname");
            ViewBag.issued_by = new SelectList(db.Employee_Details, "mine_number", "surname");
            ViewBag.return_status = new SelectList(db.Return_Status, "status_ID", "status");
            ViewBag.request_ID = new SelectList(db.Request_Details, "request_ID", "stock_code");
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw");
            ViewBag.transaction_type_ID = new SelectList(db.Type_of_Transaction, "transaction_type_ID", "type_description");
            return View();
        }

        // POST: Movements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,movement_ID,stock_code,compartment_ID,request_ID,quantity_supplied,issued_by,received_by,transaction_type_ID,date_received,return_status,expected_return_date,return_date")] Movement movement)
        {
            if (ModelState.IsValid)
            {
                db.Movements.Add(movement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.received_by = new SelectList(db.Employee_Details, "mine_number", "surname", movement.received_by);
            ViewBag.issued_by = new SelectList(db.Employee_Details, "mine_number", "surname", movement.issued_by);
            ViewBag.return_status = new SelectList(db.Return_Status, "status_ID", "status", movement.return_status);
            ViewBag.request_ID = new SelectList(db.Request_Details, "request_ID", "stock_code", movement.request_ID);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", movement.stock_code);
            ViewBag.transaction_type_ID = new SelectList(db.Type_of_Transaction, "transaction_type_ID", "type_description", movement.transaction_type_ID);
            return View(movement);
        }

        // GET: Movements/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return HttpNotFound();
            }
            ViewBag.received_by = new SelectList(db.Employee_Details, "mine_number", "surname", movement.received_by);
            ViewBag.issued_by = new SelectList(db.Employee_Details, "mine_number", "surname", movement.issued_by);
            ViewBag.return_status = new SelectList(db.Return_Status, "status_ID", "status", movement.return_status);
            ViewBag.request_ID = new SelectList(db.Request_Details, "request_ID", "stock_code", movement.request_ID);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", movement.stock_code);
            ViewBag.transaction_type_ID = new SelectList(db.Type_of_Transaction, "transaction_type_ID", "type_description", movement.transaction_type_ID);
            return View(movement);
        }

        // POST: Movements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,movement_ID,stock_code,compartment_ID,request_ID,quantity_supplied,issued_by,received_by,transaction_type_ID,date_received,return_status,expected_return_date,return_date")] Movement movement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.received_by = new SelectList(db.Employee_Details, "mine_number", "surname", movement.received_by);
            ViewBag.issued_by = new SelectList(db.Employee_Details, "mine_number", "surname", movement.issued_by);
            ViewBag.return_status = new SelectList(db.Return_Status, "status_ID", "status", movement.return_status);
            ViewBag.request_ID = new SelectList(db.Request_Details, "request_ID", "stock_code", movement.request_ID);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", movement.stock_code);
            ViewBag.transaction_type_ID = new SelectList(db.Type_of_Transaction, "transaction_type_ID", "type_description", movement.transaction_type_ID);
            return View(movement);
        }

        // GET: Movements/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return HttpNotFound();
            }
            return View(movement);
        }

        // POST: Movements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Movement movement = db.Movements.Find(id);
            db.Movements.Remove(movement);
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
