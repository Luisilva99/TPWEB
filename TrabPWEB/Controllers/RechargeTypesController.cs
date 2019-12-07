using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabPWEB.DAL;
using TrabPWEB.Models;

namespace TrabPWEB.Controllers
{
    [Authorize(Roles = "Admin,Owner")]
    public class RechargeTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RechargeTypes
        public ActionResult Index()
        {
            return View(db.RechargeTypes.ToList());
        }

        // GET: RechargeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RechargeType rechargeType = db.RechargeTypes.Find(id);
            if (rechargeType == null)
            {
                return HttpNotFound();
            }
            return View(rechargeType);
        }

        // GET: RechargeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RechargeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RechargeTypeId,RechargeTypeName,RechargeTypeDescription")] RechargeType rechargeType)
        {
            if (ModelState.IsValid)
            {
                db.RechargeTypes.Add(rechargeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rechargeType);
        }

        // GET: RechargeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RechargeType rechargeType = db.RechargeTypes.Find(id);
            if (rechargeType == null)
            {
                return HttpNotFound();
            }
            return View(rechargeType);
        }

        // POST: RechargeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RechargeTypeId,RechargeTypeName,RechargeTypeDescription")] RechargeType rechargeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rechargeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rechargeType);
        }

        // GET: RechargeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RechargeType rechargeType = db.RechargeTypes.Find(id);
            if (rechargeType == null)
            {
                return HttpNotFound();
            }
            return View(rechargeType);
        }

        // POST: RechargeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RechargeType rechargeType = db.RechargeTypes.Find(id);
            db.RechargeTypes.Remove(rechargeType);
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
