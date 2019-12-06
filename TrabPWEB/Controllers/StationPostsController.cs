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
    public class StationPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StationPosts
        public ActionResult Index()
        {
            return View(db.StationPosts.ToList());
        }

        // GET: StationPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationPost stationPost = db.StationPosts.Find(id);
            if (stationPost == null)
            {
                return HttpNotFound();
            }
            return View(stationPost);
        }

        // GET: StationPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StationPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StationPostId,StationPostName")] StationPost stationPost)
        {
            if (ModelState.IsValid)
            {
                stationPost.TimeData = new List<TimeData>();
                //Refazer este:
                for(int i = 0; i < 24; i++)
                {
                    TimeData tt = new TimeData()
                    {
                        Time = new DateTime(1999,1,1,i,0,0),
                        Status = false,
                        TimeDataId = db.TimeDatas.Count()
                    };
                    stationPost.TimeData.Add(tt);
                }
                
                db.StationPosts.Add(stationPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stationPost);
        }

        // GET: StationPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationPost stationPost = db.StationPosts.Find(id);
            if (stationPost == null)
            {
                return HttpNotFound();
            }
            return View(stationPost);
        }

        // POST: StationPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StationPostId,StationPostName")] StationPost stationPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stationPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stationPost);
        }

        // GET: StationPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationPost stationPost = db.StationPosts.Find(id);
            if (stationPost == null)
            {
                return HttpNotFound();
            }
            foreach (TimeData item in db.TimeDatas.ToList())
            {
                if (stationPost.TimeData.Contains(item))
                {
                    stationPost.TimeData.Remove(item);
                    db.TimeDatas.Remove(item);
                }
            }
            db.SaveChanges();
            stationPost.TimeData.Clear();
            return View(stationPost);
        }

        // POST: StationPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StationPost stationPost = db.StationPosts.Find(id);
            db.StationPosts.Remove(stationPost);
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
