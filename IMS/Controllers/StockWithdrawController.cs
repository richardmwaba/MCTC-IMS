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
    public class StockWithdrawController : Controller
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: StockWithdraw
        public ActionResult Index()
        {
            var stock_Withdraw = db.Stock_Withdraw.Include(s => s.Employee_Details).Include(s => s.Shelf_Compartment).Include(s => s.Stock_Category).Include(s => s.Stock_Details);
            return View(stock_Withdraw.ToList());
        }

        // GET: StockWithdraw/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Withdraw stock_Withdraw = db.Stock_Withdraw.Find(id);
            if (stock_Withdraw == null)
            {
                return HttpNotFound();
            }
            return View(stock_Withdraw);
        }

        // GET: StockWithdraw/Create
        public ActionResult Create()
        {
            ViewBag.withdrawer = new SelectList(db.Employee_Details, "mine_number", "surname");
            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID");
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description");
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw");
            return View();
        }

        // POST: StockWithdraw/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,withdraw_ID,stock_code,quantity,unit_of_withdraw,withdrawer,date_of_withdraw,compartment_ID,stock_type,expiry_date,category_ID")] Stock_Withdraw stock_Withdraw)
        {
            if (ModelState.IsValid)
            {
                db.Stock_Withdraw.Add(stock_Withdraw);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.withdrawer = new SelectList(db.Employee_Details, "mine_number", "surname", stock_Withdraw.withdrawer);
            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID", stock_Withdraw.compartment_ID);
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description", stock_Withdraw.category_ID);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", stock_Withdraw.stock_code);
            return View(stock_Withdraw);
        }

        // GET: StockWithdraw/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Withdraw stock_Withdraw = db.Stock_Withdraw.Find(id);
            if (stock_Withdraw == null)
            {
                return HttpNotFound();
            }
            ViewBag.withdrawer = new SelectList(db.Employee_Details, "mine_number", "surname", stock_Withdraw.withdrawer);
            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID", stock_Withdraw.compartment_ID);
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description", stock_Withdraw.category_ID);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", stock_Withdraw.stock_code);
            return View(stock_Withdraw);
        }

        // POST: StockWithdraw/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,withdraw_ID,stock_code,quantity,unit_of_withdraw,withdrawer,date_of_withdraw,compartment_ID,stock_type,expiry_date,category_ID")] Stock_Withdraw stock_Withdraw)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock_Withdraw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.withdrawer = new SelectList(db.Employee_Details, "mine_number", "surname", stock_Withdraw.withdrawer);
            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID", stock_Withdraw.compartment_ID);
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description", stock_Withdraw.category_ID);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", stock_Withdraw.stock_code);
            return View(stock_Withdraw);
        }

        // GET: StockWithdraw/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Withdraw stock_Withdraw = db.Stock_Withdraw.Find(id);
            if (stock_Withdraw == null)
            {
                return HttpNotFound();
            }
            return View(stock_Withdraw);
        }

        // POST: StockWithdraw/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stock_Withdraw stock_Withdraw = db.Stock_Withdraw.Find(id);
            db.Stock_Withdraw.Remove(stock_Withdraw);
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
