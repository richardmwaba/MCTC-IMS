using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;
using IMS.ViewModel;

namespace IMS.Controllers
{
    public class StockDetailsController : Controller
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: StockDetails
        public ActionResult Index()
        {
            var stock_Details = db.Stock_Details.Include(s => s.Shelf_Compartment).Include(s => s.Stock_Availability).Include(s => s.Stock_Category).Include(s => s.Stock_Type1);
            return View(stock_Details.ToList());
        }

        public ActionResult AllStock()
        {
            var model = db.Stock_Category;
            return View(model);
        }
        
        // GET: Stock under a chosen category
        [ChildActionOnly]
        public PartialViewResult StockInCategory (string id)
        {
            var data = (from s in db.Stock_Details where s.category_ID == id orderby s.date_of_order select s).Take(5);
            return PartialView(data);
        }

        // GET: details of selected stock category
        public ActionResult ViewStock(string id)
        {
            var category = db.Stock_Category.Where(o => o.category_ID == id).Select(q => q.description).SingleOrDefault();
            ViewBag.Category = category;
            var stock_Details = db.getStockByCategory(id);
            return View(stock_Details);
        }

        //GET: all available stock
        public ActionResult AvailableStock()
        {
            var cate = db.Stock_Category.Take(5);
            return View(cate);
            
        }

        //GET: all available stock under a chosen category
        public PartialViewResult StockAvailableCategory(string id)
        {
            var available_stock = db.getAvailableStock(id).Take(5);
            //getAvailableStock_Result available_stock = null;

            //else
            //{
            //    IEnumerable<getAvailableStock_Result> available_stock = new IEnumerable<getAvailableStock_Result>
            //    {
            //        stock_code = "",
            //        description_of_items = "",
            //        quantity_available = 0,
            //        unit_of_issue = "",
            //        stock_type = "",
            //        expiry_date = DateTime.MinValue,
            //        compartment_ID = "",
            //        shelf = "",
            //        stock_category = "",
            //        status = "",
            //    };
            //}
            return PartialView(available_stock);
        }

        //GET: all available stock under a chosen category to be viewed on a modal
        public PartialViewResult ModalAvailableStock(string id)
        {
            var stock = db.getAvailableStock(id);
            return PartialView(stock);
        }
           
        // GET: StockDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Details stock_Details = db.Stock_Details.Find(id);
            if (stock_Details == null)
            {
                return HttpNotFound();
            }
            return View(stock_Details);
        }

        // GET: StockDetails/Create
        public ActionResult Create()
        {
            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID");
            ViewBag.availability = new SelectList(db.Stock_Availability, "availability_ID", "status");
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description");
            ViewBag.stock_type = new SelectList(db.Stock_Type, "type_ID", "description");
            return View();
        }

        // POST: StockDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,stock_code,quantity_available,unit_of_withdraw,description_of_items,unit_of_issue,reorder_level,minimum_level,date_of_order,expiry_date,stock_type,compartment_ID,category_ID,availability")] Stock_Details stock_Details)
        {
            if (ModelState.IsValid)
            {
                db.Stock_Details.Add(stock_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID", stock_Details.compartment_ID);
            ViewBag.availability = new SelectList(db.Stock_Availability, "availability_ID", "status", stock_Details.availability);
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description", stock_Details.category_ID);
            ViewBag.stock_type = new SelectList(db.Stock_Type, "type_ID", "description", stock_Details.stock_type);
            return View(stock_Details);
        }

        // GET: StockDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Details stock_Details = db.Stock_Details.Find(id);
            if (stock_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID", stock_Details.compartment_ID);
            ViewBag.availability = new SelectList(db.Stock_Availability, "availability_ID", "status", stock_Details.availability);
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description", stock_Details.category_ID);
            ViewBag.stock_type = new SelectList(db.Stock_Type, "type_ID", "description", stock_Details.stock_type);
            return View(stock_Details);
        }

        // POST: StockDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,stock_code,quantity_available,unit_of_withdraw,description_of_items,unit_of_issue,reorder_level,minimum_level,date_of_order,expiry_date,stock_type,compartment_ID,category_ID,availability")] Stock_Details stock_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.compartment_ID = new SelectList(db.Shelf_Compartment, "compartment_ID", "shelf_ID", stock_Details.compartment_ID);
            ViewBag.availability = new SelectList(db.Stock_Availability, "availability_ID", "status", stock_Details.availability);
            ViewBag.category_ID = new SelectList(db.Stock_Category, "category_ID", "description", stock_Details.category_ID);
            ViewBag.stock_type = new SelectList(db.Stock_Type, "type_ID", "description", stock_Details.stock_type);
            return View(stock_Details);
        }

        // GET: StockDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Details stock_Details = db.Stock_Details.Find(id);
            if (stock_Details == null)
            {
                return HttpNotFound();
            }
            return View(stock_Details);
        }

        // POST: StockDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stock_Details stock_Details = db.Stock_Details.Find(id);
            db.Stock_Details.Remove(stock_Details);
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
