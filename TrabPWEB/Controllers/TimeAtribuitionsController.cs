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
    public class TimeAtribuitionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeAtribuitions
        public ActionResult Index()
        {
            var timeAtribuitions = db.TimeAtribuitions.Include(t => t.StationPost).Include(t => t.TimeData);
            return View(timeAtribuitions.ToList());
        }

        // GET: TimeAtribuitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeAtribuition timeAtribuition = db.TimeAtribuitions.Find(id);
            if (timeAtribuition == null)
            {
                return HttpNotFound();
            }
            return View(timeAtribuition);
        }

        // GET: TimeAtribuitions/Create
        public ActionResult Create()
        {
            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName");
            ViewBag.TimeDataId = new SelectList(db.TimeDatas, "TimeDataId", "TimeDataId");
            return View();
        }

        // POST: TimeAtribuitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeDataId,StationPostId")] TimeAtribuition timeAtribuition)
        {
            if (ModelState.IsValid)
            {
                db.TimeAtribuitions.Add(timeAtribuition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName", timeAtribuition.StationPostId);
            ViewBag.TimeDataId = new SelectList(db.TimeDatas, "TimeDataId", "TimeDataId", timeAtribuition.TimeDataId);
            return View(timeAtribuition);
        }

        // GET: TimeAtribuitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeAtribuition timeAtribuition = db.TimeAtribuitions.Find(id);
            if (timeAtribuition == null)
            {
                return HttpNotFound();
            }
            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName", timeAtribuition.StationPostId);
            ViewBag.TimeDataId = new SelectList(db.TimeDatas, "TimeDataId", "TimeDataId", timeAtribuition.TimeDataId);
            return View(timeAtribuition);
        }

        // POST: TimeAtribuitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeDataId,StationPostId")] TimeAtribuition timeAtribuition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeAtribuition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName", timeAtribuition.StationPostId);
            ViewBag.TimeDataId = new SelectList(db.TimeDatas, "TimeDataId", "TimeDataId", timeAtribuition.TimeDataId);
            return View(timeAtribuition);
        }

        // GET: TimeAtribuitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeAtribuition timeAtribuition = db.TimeAtribuitions.Find(id);
            if (timeAtribuition == null)
            {
                return HttpNotFound();
            }
            return View(timeAtribuition);
        }

        // POST: TimeAtribuitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeAtribuition timeAtribuition = db.TimeAtribuitions.Find(id);
            db.TimeAtribuitions.Remove(timeAtribuition);
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
