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
    public class RequestDetailsController : Controller
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: RequestDetails
        public ActionResult Index()
        {
            var request_Details = db.Request_Details.Include(r => r.Approval_Status1).Include(r => r.Employee_Details).Include(r => r.Employee_Details1).Include(r => r.Stock_Details);
            return View(request_Details.ToList());
        }

        // GET: RequestDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request_Details request_Details = db.Request_Details.Find(id);
            if (request_Details == null)
            {
                return HttpNotFound();
            }
            return View(request_Details);
        }

        // GET: All approved requests
        public ActionResult Approved()
        {
            var model = db.getAllApprovedORDeniedRequests(2);
            return View(model);
        }

        // GET: All rejected requests
        public ActionResult Rejected()
        {
            var model = db.getAllApprovedORDeniedRequests(4);
            return View(model);
        }

        //GET: All open requests
        public ActionResult Open()
        {
            var model = db.getAllApprovedORDeniedRequests(0);
            return View(model);
        }

        // GET: RequestDetails/Create
        public ActionResult Create()
        {
            Random r = new Random();
            int rInt = r.Next(0, 10000);
            string requestId = "REQ" + rInt;

            ViewBag.request_ID = requestId;
            ViewBag.approval_status = new SelectList(db.Approval_Status, "approval_ID", "status");
            ViewBag.mine_number = new SelectList(db.Employee_Details, "mine_number", "surname");
            ViewBag.approver = new SelectList(db.Employee_Details, "mine_number", "surname");
            ViewBag.stock_name = new SelectList(db.Stock_Details, "stock_code", "description_of_items");
            return View();
        }

        public string getCompartment(string id)
        {
            var compartment = db.Stock_Details.Where(x => x.stock_code == id).Select(x => x.compartment_ID).SingleOrDefault();
            return compartment;
        }
        
        public string getUnit(string id)
        {
            var unit = db.Stock_Details.Where(x => x.stock_code == id).Select(x => x.unit_of_issue).SingleOrDefault();
            return unit;
        }

        // POST: Custom method for creating a request
        public void createRequest(string request_ID, string stock_code, string compartment_ID, string purpose_of_item, string mine_number, string date_of_request, int quantity,string unit_of_issue, int approval_status, string approver)
        {
            if(ModelState.IsValid)
            {
                db.insertRequest(
                    request_ID, 
                    stock_code, 
                    compartment_ID, 
                    purpose_of_item, 
                    mine_number, 
                    date_of_request, 
                    quantity, 
                    unit_of_issue, 
                    approval_status, 
                    approver
                );
            }
            
        }
        // POST: RequestDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,request_ID,stock_code,compartment_ID,purpose_of_item,mine_number,date_of_request,quantity,approval_status,approver")] Request_Details request_Details)
        {
            if (ModelState.IsValid)
            {
                db.Request_Details.Add(request_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.approval_status = new SelectList(db.Approval_Status, "approval_ID", "status", request_Details.approval_status);
            ViewBag.mine_number = new SelectList(db.Employee_Details, "mine_number", "surname", request_Details.mine_number);
            ViewBag.approver = new SelectList(db.Employee_Details, "mine_number", "surname", request_Details.approver);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", request_Details.stock_code);
            return View(request_Details);
        }

        // GET: RequestDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request_Details request_Details = db.Request_Details.Find(id);
            if (request_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.approval_status = new SelectList(db.Approval_Status, "approval_ID", "status", request_Details.approval_status);
            ViewBag.mine_number = new SelectList(db.Employee_Details, "mine_number", "surname", request_Details.mine_number);
            ViewBag.approver = new SelectList(db.Employee_Details, "mine_number", "surname", request_Details.approver);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", request_Details.stock_code);
            return View(request_Details);
        }

        // POST: RequestDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,request_ID,stock_code,compartment_ID,purpose_of_item,mine_number,date_of_request,quantity,approval_status,approver")] Request_Details request_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.approval_status = new SelectList(db.Approval_Status, "approval_ID", "status", request_Details.approval_status);
            ViewBag.mine_number = new SelectList(db.Employee_Details, "mine_number", "surname", request_Details.mine_number);
            ViewBag.approver = new SelectList(db.Employee_Details, "mine_number", "surname", request_Details.approver);
            ViewBag.stock_code = new SelectList(db.Stock_Details, "stock_code", "unit_of_withdraw", request_Details.stock_code);
            return View(request_Details);
        }

        // GET: RequestDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request_Details request_Details = db.Request_Details.Find(id);
            if (request_Details == null)
            {
                return HttpNotFound();
            }
            return View(request_Details);
        }

        // POST: RequestDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Request_Details request_Details = db.Request_Details.Find(id);
            db.Request_Details.Remove(request_Details);
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
