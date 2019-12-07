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
    public class RechargeModsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RechargeMods
        public ActionResult Index()
        {
            return View(db.RechargeMods.ToList());
        }

        // GET: RechargeMods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RechargeMod rechargeMod = db.RechargeMods.Find(id);
            if (rechargeMod == null)
            {
                return HttpNotFound();
            }
            return View(rechargeMod);
        }

        // GET: RechargeMods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RechargeMods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RechargeModId,RechargeModName,RechargeModDescription")] RechargeMod rechargeMod)
        {
            if (ModelState.IsValid)
            {
                db.RechargeMods.Add(rechargeMod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rechargeMod);
        }

        // GET: RechargeMods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RechargeMod rechargeMod = db.RechargeMods.Find(id);
            if (rechargeMod == null)
            {
                return HttpNotFound();
            }
            return View(rechargeMod);
        }

        // POST: RechargeMods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RechargeModId,RechargeModName,RechargeModDescription")] RechargeMod rechargeMod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rechargeMod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rechargeMod);
        }

        // GET: RechargeMods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RechargeMod rechargeMod = db.RechargeMods.Find(id);
            if (rechargeMod == null)
            {
                return HttpNotFound();
            }
            return View(rechargeMod);
        }

        // POST: RechargeMods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RechargeMod rechargeMod = db.RechargeMods.Find(id);
            db.RechargeMods.Remove(rechargeMod);
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
