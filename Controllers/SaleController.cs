using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SECURETEST.Models;

namespace SECURETEST.Controllers
{
    public class SaleController : Controller
    {
        private readonly SECURESOFT db = new SECURESOFT();

        // GET: Sale
        public ActionResult Index()
        {
            return View(db.SALEs.ToList());
        }

        // GET: Sale/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALEs.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            return View(sALE);
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            return View(new SALE());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SALE sALE)
        {
            if (ModelState.IsValid)
            {
                sALE.INVOICE_NO = GetInvoiceNo();
                db.SALEs.Add(sALE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sALE);
        }
        private  string GetInvoiceNo()
        {
            var seqVal = db.Database.SqlQuery<long>("SELECT NEXT VALUE FOR INVOICE_SEQ;").First();
            return "INV"+ seqVal.ToString().PadLeft(3,'0');
        }
        // GET: Sale/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALEs.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            return View(sALE);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SALE sALE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sALE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sALE);
        }

        // GET: Sale/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALEs.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            return View(sALE);
        }

        // POST: Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SALE sALE = db.SALEs.Find(id);
            db.SALEs.Remove(sALE);
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
        public JsonResult GetSaleItems(string search)
        {
            try
            {
               var searchResult = db.STOCKs.AsNoTracking().Where(x => x.ITEM_NAME.Contains(search)).Select(x => new
                {
                   itemName= x.ITEM_NAME,
                   itemId=x.ITEM_ID
                }).Distinct().AsParallel().ToList();
                return new JsonResult { Data = searchResult, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception)
            {
                return new JsonResult { Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}
