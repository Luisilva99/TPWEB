﻿using System;
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

            foreach (var item in db.Reserves)
            {

                DateTime date = item.Date;

                if (date.AddHours(1.0) < DateTime.Now && item.Completed == 1)
                {
                    //Completação da reserva
                    item.Completed = 2;
                    //-----------------------

                    //Reatribuição do horário livre ao Posto de Carregamento
                    TimeAtribuition time = db.TimeAtribuitions.Where(o => o.TimeData.Time.Hour == item.Date.Hour && o.StationPostId == item.StationPostId).Single();

                    TimeData timeData = db.TimeDatas.Where(o => o.Status == true && o.Time.Hour == time.TimeData.Time.Hour).Single();

                    db.TimeAtribuitions.Remove(time);

                    TimeAtribuition ta = new TimeAtribuition()
                    {
                        TimeData = timeData,
                        TimeDataId = timeData.TimeDataId,
                        StationPost = item.StationPost,
                        StationPostId = item.StationPost.StationPostId
                    };

                    db.TimeAtribuitions.Add(ta);
                    //------------------------------------------------------

                }
            }

            db.SaveChanges();

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
            if (NomePostoRepetido(stationPost))
            {
                ModelState.AddModelError("StationPostName", "Já existe um posto com este nome nesta estação.");
            }

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
        public ActionResult Edit([Bind(Include = "StationPostId,StationPostName,RechargeTypeId,Start,Finnish,Price")] StationPost stationPost)
        {

            StationPost sst = db.StationPosts.Find(stationPost.StationPostId);
            
            
            if (ModelState.IsValid)
            {

                //if (sst.StationPostName.Equals(stationPost.StationPostName))
                //{
                //    ModelState.AddModelError("StationPostName", "Já existe um posto com este nome nesta estação.");
                //    ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName", stationPost.RechargeTypeId);
                //    return View(stationPost);
                //}

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

        // Validação
        [NonAction]
        private bool NomePostoRepetido(StationPost s)
        {
            foreach (var aux in db.StationPosts)
            {
                if (aux.StationPostName.Equals(s.StationPostName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
