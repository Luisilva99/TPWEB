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
    public class ReservesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reserves
        public ActionResult Index()
        {
            var reserves = db.Reserves.Include(r => r.RechargeMod).Include(r => r.StationPost);
            string userId = db.Users.Where(o => o.UserName.Equals(User.Identity.Name)).Select(o => o.Id).Single();
            return View(reserves.Where(o => o.UserId.Equals(userId)).ToList());
        }

        // GET: Reserves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserves.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            return View(reserve);
        }

        // GET: Reserves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserves.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            ViewBag.RechargeModId = new SelectList(db.RechargeMods, "RechargeModId", "RechargeModName", reserve.RechargeModId);
            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName", reserve.StationPostId);
            return View(reserve);
        }

        // POST: Reserves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReserveId,UserId,StationPostId,RechargeModId,Date,Completed")] Reserve reserve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RechargeModId = new SelectList(db.RechargeMods, "RechargeModId", "RechargeModName", reserve.RechargeModId);
            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName", reserve.StationPostId);
            return View(reserve);
        }

        // GET: Reserves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserves.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            return View(reserve);
        }

        // POST: Reserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserve reserve = db.Reserves.Find(id);
            db.Reserves.Remove(reserve);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [NonAction]
        public string getUserName(string userId)
        {
            string name = db.Users.Where(o => o.Id.Equals(userId)).Select(o => o.UserName).Single();

            if(name == null)
            {
                return "[Deleted]";
            }
            return name;
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
