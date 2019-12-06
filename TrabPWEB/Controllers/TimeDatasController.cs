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
    public class TimeDatasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeDatas
        public ActionResult Index()
        {
            var ordemTime = db.TimeDatas.ToList().OrderBy(p => p.Time.Hour).ThenBy(p => p.Time.Minute);
            return View(ordemTime);
        }

        // GET: TimeDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeData timeData = db.TimeDatas.Find(id);
            if (timeData == null)
            {
                return HttpNotFound();
            }
            return View(timeData);
        }

        // GET: TimeDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeDataId,Time,Status")] TimeData timeData)
        {
            if (ModelState.IsValid)
            {
                db.TimeDatas.Add(timeData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeData);
        }

        // GET: TimeDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeData timeData = db.TimeDatas.Find(id);
            if (timeData == null)
            {
                return HttpNotFound();
            }
            return View(timeData);
        }

        // POST: TimeDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeDataId,Time,Status")] TimeData timeData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeData);
        }

        // GET: TimeDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeData timeData = db.TimeDatas.Find(id);
            if (timeData == null)
            {
                return HttpNotFound();
            }
            return View(timeData);
        }

        // POST: TimeDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeData timeData = db.TimeDatas.Find(id);
            db.TimeDatas.Remove(timeData);
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
