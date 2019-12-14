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
using TrabPWEB.ViewModels;

namespace TrabPWEB.Controllers
{
    public class StationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stations
        public ActionResult Index(string local, string procura, int? pagina)
        {
            StationsIndexViewModel svm = new StationsIndexViewModel();

            var stations = db.Stations.Include(s => s.Local);

            if (!String.IsNullOrEmpty(procura))
            {
                stations = stations.Where(s => s.Local.LocalName.Contains(procura) || s.StationName.Contains(procura));
                svm.Procura = procura;
            }
            else
            {
                // AJUDA - É preciso que quando se clique no "Pesquisar" quando não tem nada (null) fique na mesma página, ou seja, Home/Index
                //         Porém o Redirect e assim dão mas depois se quiser ir às Stations pelo URL não dá :/
            }

            int nreg = 5;
            int pag = (pagina ?? 1); //Se pagina for um valor não nulo pag= pagina; senão pag= 1.

            svm.OrdenarEstacoes(stations, pag, nreg);

            return View(svm);
        }

        // GET: StationPosts/AddPost
        public ActionResult AddPost(int? id)
        {

            if (id == null || id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Station stat = db.Stations.Find(id);

            ViewBag.Tid = id;

            ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName");

            StationPost post = new StationPost()
            {
                Start = stat.Start,
                Finnish = stat.Finnish
            };

            return View(post);
        }

        // POST: StationPosts/AddPost
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(StationPost stationPost, int? StationId)
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

                Station stat = db.Stations.Find(StationId);

                StationPostsAtribuition sss = new StationPostsAtribuition()
                {
                    Station = stat,
                    StationId = stat.StationId,
                    StationPost = stationPost,
                    StationPostId = stationPost.StationPostId
                };

                db.StationPostsAtribuition.Add(sss);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName", stationPost.RechargeTypeId);
            return View(stationPost);
        }


        public List<StationPost> getPostos(int? id)
        {
            var truePostos = db.StationPostsAtribuition.Where(o => o.StationId == id).Select(l => l.StationPost);
            return truePostos.ToList();
        }


        public List<TimeData> getStationTimes(int? id)
        {
            var ttt = db.TimeAtribuitions.Where(o => o.StationPostId == id).Select(l => l.TimeData);
            return ttt.ToList();
        }

        // GET: Stations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // GET: Stations/Create
        public ActionResult Create()
        {
            ViewBag.LocalId = new SelectList(db.Locals, "LocalId", "LocalName");
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Station station, string UserName)
        {
            if (ModelState.IsValid)
            {

                //Atribuição ao Owner da Estação de carregamento
                var user = db.Users.Where(o => o.UserName.Equals(UserName)).Single();
                StationAtribution sa = new StationAtribution()
                {
                    UserId = user.Id,
                    Station = station,
                    StationId = station.StationId
                };
                db.StationAtributions.Add(sa);
                //----------------------------------------------

                db.Stations.Add(station);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocalId = new SelectList(db.Locals, "LocalId", "LocalName", station.LocalId);
            return View(station);
        }

        // GET: Stations/Edit/5
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocalId = new SelectList(db.Locals, "LocalId", "LocalName", station.LocalId);
            return View(station);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StationId,StationName,LocalId,Start,Finnish")] Station station)
        {
            if (ModelState.IsValid)
            {

                var stationPost = db.StationPostsAtribuition.Where(o => o.StationId == station.StationId).Select(o => o.StationPostId);
                
                if(stationPost != null)
                {
                    foreach (int idPost in stationPost)
                    {
                        var times = db.TimeAtribuitions.Where(o => o.StationPostId == idPost);

                        db.TimeAtribuitions.RemoveRange(times);

                        db.StationPosts.Find(idPost).Start = station.Start;
                        db.StationPosts.Find(idPost).Finnish = station.Finnish;

                        for (int i = 0; i < db.TimeDatas.Count() / 2; i++)
                        {
                            if (i < db.StationPosts.Find(idPost).Start.Hour || i > db.StationPosts.Find(idPost).Finnish.Hour)
                            {
                                var trueTimes = db.TimeDatas.Where(o => o.Time.Hour == i).Where(k => k.Status == false).Single();
                                TimeAtribuition ta = new TimeAtribuition()
                                {
                                    StationPostId = db.StationPosts.Find(idPost).StationPostId,
                                    StationPost = db.StationPosts.Find(idPost),
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
                                    StationPostId = db.StationPosts.Find(idPost).StationPostId,
                                    StationPost = db.StationPosts.Find(idPost),
                                    TimeData = trueTimes,
                                    TimeDataId = trueTimes.TimeDataId
                                };

                                db.TimeAtribuitions.Add(ta);
                            }
                        }
                        db.Entry(db.StationPosts.Find(idPost)).State = EntityState.Modified;
                    }
                }

                db.Entry(station).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocalId = new SelectList(db.Locals, "LocalId", "LocalName", station.LocalId);
            return View(station);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string UserName)
        {
            if (UserName == null || UserName.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Station station = db.Stations.Find(id);

            //Eliminação da atribuição do Owner à estação de carregamento
            var user = db.Users.Where(o => o.UserName.Equals(UserName)).Single();
            StationAtribution sa = db.StationAtributions.Where(o => o.UserId.Equals(user.Id)).Single();
            db.StationAtributions.Remove(sa);
            //-----------------------------------------------------------

            var stationPost = db.StationPostsAtribuition.Where(o => o.StationId == station.StationId).Select(o => o.StationPostId);

            if(stationPost != null)
            {
                foreach (int idPost in stationPost)
                {
                    var times = db.TimeAtribuitions.Where(o => o.StationPostId == idPost);

                    db.TimeAtribuitions.RemoveRange(times);
                }
            }

            var stationPostTrue = db.StationPostsAtribuition.Where(o => o.StationId == station.StationId).Select(o => o.StationPost);

            if(stationPostTrue != null)
            {
                db.StationPosts.RemoveRange(stationPostTrue);
            }

            var stationsAtt = db.StationPostsAtribuition.Where(o => o.StationId == station.StationId);

            if(stationsAtt != null)
            {
                db.StationPostsAtribuition.RemoveRange(stationsAtt);
            }

            db.Stations.Remove(station);
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
