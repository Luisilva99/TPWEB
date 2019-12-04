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
    public class TimeHoursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeHours
        public ActionResult Index()
        {
            return View(db.TimeHours.ToList());
        }

        // GET: TimeHours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeHour timeHour = db.TimeHours.Find(id);
            if (timeHour == null)
            {
                return HttpNotFound();
            }
            return View(timeHour);
        }

        // GET: TimeHours/Create
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeHours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeHourId,Time,Status")] TimeHour timeHour)
        {
            if (ModelState.IsValid)
            {
                db.TimeHours.Add(timeHour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeHour);
        }

        // GET: TimeHours/Edit/5
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeHour timeHour = db.TimeHours.Find(id);
            if (timeHour == null)
            {
                return HttpNotFound();
            }
            return View(timeHour);
        }

        // POST: TimeHours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeHourId,Time,Status")] TimeHour timeHour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeHour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeHour);
        }

        // GET: TimeHours/Delete/5
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeHour timeHour = db.TimeHours.Find(id);
            if (timeHour == null)
            {
                return HttpNotFound();
            }
            return View(timeHour);
        }

        // POST: TimeHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeHour timeHour = db.TimeHours.Find(id);
            db.TimeHours.Remove(timeHour);
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
