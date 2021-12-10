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
    public class SaleRepoController : Controller
    {
        private readonly SECURESOFT db = new SECURESOFT();

        // GET: SaleRepo
        public ActionResult Index()
        {
            var sALE_REPO = db.SALE_REPO.Include(s => s.SALE).Include(s => s.STOCK);
            return View(sALE_REPO.ToList());
        }

        // GET: SaleRepo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE_REPO sALE_REPO = db.SALE_REPO.Find(id);
            if (sALE_REPO == null)
            {
                return HttpNotFound();
            }
            return View(sALE_REPO);
        }

        // GET: SaleRepo/Create
        public ActionResult Create()
        {
            ViewBag.SALE_INVOICE = new SelectList(db.SALEs, "INVOICE_NO", "INVOICE_NO");
            ViewBag.ITEM_ID = new SelectList(db.STOCKs, "ITEM_ID", "ITEM_NAME");
            return View(new SALE_REPO());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SALE_REPO sALE_REPO)
        {
            if (ModelState.IsValid)
            {
                db.SALE_REPO.Add(sALE_REPO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SALE_INVOICE = new SelectList(db.SALEs, "INVOICE_NO", "INVOICE_NO", sALE_REPO.SALE_INVOICE);
            ViewBag.ITEM_ID = new SelectList(db.STOCKs, "ITEM_ID", "ITEM_NAME", sALE_REPO.ITEM_ID);
            return View(sALE_REPO);
        }

        // GET: SaleRepo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE_REPO sALE_REPO = db.SALE_REPO.Find(id);
            if (sALE_REPO == null)
            {
                return HttpNotFound();
            }
            ViewBag.SALE_INVOICE = new SelectList(db.SALEs, "INVOICE_NO", "INVOICE_NO", sALE_REPO.SALE_INVOICE);
            ViewBag.ITEM_ID = new SelectList(db.STOCKs, "ITEM_ID", "ITEM_NAME", sALE_REPO.ITEM_ID);
            return View(sALE_REPO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SALE_REPO sALE_REPO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sALE_REPO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SALE_INVOICE = new SelectList(db.SALEs, "INVOICE_NO", "INVOICE_NO", sALE_REPO.SALE_INVOICE);
            ViewBag.ITEM_ID = new SelectList(db.STOCKs, "ITEM_ID", "ITEM_NAME", sALE_REPO.ITEM_ID);
            return View(sALE_REPO);
        }

        // GET: SaleRepo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE_REPO sALE_REPO = db.SALE_REPO.Find(id);
            if (sALE_REPO == null)
            {
                return HttpNotFound();
            }
            return View(sALE_REPO);
        }

        // POST: SaleRepo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SALE_REPO sALE_REPO = db.SALE_REPO.Find(id);
            db.SALE_REPO.Remove(sALE_REPO);
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
