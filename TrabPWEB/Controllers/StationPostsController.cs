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
            var stationPosts = db.StationPosts.Include(s => s.RechargeType).OrderBy(o => o.StationPostId);
            return View(stationPosts.ToList());
        }

        // GET: Tempos do posto
        public List<TimeData> getStationTimes(int? id)
        {
            var ttt = db.TimeAtribuitions.Where(o => o.StationPostId == id).Select(l => l.TimeData);
            return ttt.ToList();
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
            ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName");
            return View();
        }

        // POST: StationPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StationPost stationPost)
        {
            if (ModelState.IsValid)
            {

                db.StationPosts.Add(stationPost);

                for (int i = 0; i < db.TimeDatas.Count() / 2; i++)
                {
                    if (i < stationPost.Start.Hour || i > stationPost.Finnish.Hour)
                    {
                        var trueTimes = db.TimeDatas.Where(o => o.Time.Hour == i).Where(k => k.Status == false).Single();
                        TimeAtribuition ta = new TimeAtribuition()
                        {
                            StationPostId = stationPost.StationPostId,
                            StationPost = stationPost,
                            TimeData = trueTimes,
                            TimeDataId = trueTimes.TimeDataId
                        };

                        db.TimeAtribuitions.Add(ta);
                    }
                    else
                    {
                        var trueTimes = db.TimeDatas.Where(o => o.Time.Hour == i).Where(k => k.Status == true).Single();
                        TimeAtribuition ta = new TimeAtribuition()
                        {
                            StationPostId = stationPost.StationPostId,
                            StationPost = stationPost,
                            TimeData = trueTimes,
                            TimeDataId = trueTimes.TimeDataId
                        };

                        db.TimeAtribuitions.Add(ta);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName", stationPost.RechargeTypeId);
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
            ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName", stationPost.RechargeTypeId);
            return View(stationPost);
        }

        // POST: StationPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StationPostId,StationPostName,RechargeTypeId,Start,Finnish")] StationPost stationPost)
        {
            if (ModelState.IsValid)
            {

                var times = db.TimeAtribuitions.Where(o => o.StationPostId == stationPost.StationPostId);

                db.TimeAtribuitions.RemoveRange(times);

                for (int i = 0; i < db.TimeDatas.Count() / 2; i++)
                {
                    if (i < stationPost.Start.Hour || i > stationPost.Finnish.Hour)
                    {
                        var trueTimes = db.TimeDatas.Where(o => o.Time.Hour == i).Where(k => k.Status == false).Single();
                        TimeAtribuition ta = new TimeAtribuition()
                        {
                            StationPostId = stationPost.StationPostId,
                            StationPost = stationPost,
                            TimeData = trueTimes,
                            TimeDataId = trueTimes.TimeDataId
                        };

                        db.TimeAtribuitions.Add(ta);
                    }
                    else
                    {
                        var trueTimes = db.TimeDatas.Where(o => o.Time.Hour == i).Where(k => k.Status == true).Single();
                        TimeAtribuition ta = new TimeAtribuition()
                        {
                            StationPostId = stationPost.StationPostId,
                            StationPost = stationPost,
                            TimeData = trueTimes,
                            TimeDataId = trueTimes.TimeDataId
                        };

                        db.TimeAtribuitions.Add(ta);
                    }
                }

                db.Entry(stationPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName", stationPost.RechargeTypeId);
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
            return View(stationPost);
        }

        // POST: StationPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var times = db.TimeAtribuitions.Where(o => o.StationPostId == id);

            db.TimeAtribuitions.RemoveRange(times);

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
