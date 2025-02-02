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

            StationsIndexViewModel svm = new StationsIndexViewModel();
            
            if (User.IsInRole("Owner"))
            {
                string userId = db.Users.Where(o => o.UserName.Equals(User.Identity.Name)).Select(o => o.Id).Single();

                var stations = db.StationAtributions.Where(o => o.UserId.Equals(userId)).Select(o => o.Station).Include(s => s.Local);

                if (!String.IsNullOrEmpty(procura))
                {
                    stations = stations.Where(s => s.Local.LocalName.Contains(procura) || s.StationName.Contains(procura));

                    svm.Procura = procura;
                }

                int nreg = 5;
                int pag = (pagina ?? 1); //Se pagina for um valor não nulo pag= pagina; senão pag= 1.

                svm.OrdenarEstacoes(stations, pag, nreg);
            }
            else
            {
                var stations = db.StationAtributions.Select(o => o.Station).Include(s => s.Local);

                if (!String.IsNullOrEmpty(procura))
                {
                    stations = stations.Where(s => s.Local.LocalName.Contains(procura) || s.StationName.Contains(procura));

                    svm.Procura = procura;
                }

                int nreg = 5;
                int pag = (pagina ?? 1); //Se pagina for um valor não nulo pag= pagina; senão pag= 1.

                svm.OrdenarEstacoes(stations, pag, nreg);
            }

            return View(svm);
        }

        // GET: StationPosts/AddPost
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult AddPost(int? id)
        {
            if (!User.IsInRole("Owner") && !User.IsInRole("Admin"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
            if (!User.IsInRole("Owner") && !User.IsInRole("Admin"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

            if (NomePostoRepetido(stationPost))
            {
                ModelState.AddModelError("StationPostName", "Já existe um posto com este nome nesta ou noutra estação.");
                AddPost(StationId);
            }
            else
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
            }

            ViewBag.RechargeTypeId = new SelectList(db.RechargeTypes, "RechargeTypeId", "RechargeTypeName", stationPost.RechargeTypeId);
            return View(stationPost);
        }


        public List<StationPost> getPostos(int? id)
        {
            var truePostos = db.StationPostsAtribuition.Where(o => o.StationId == id).Select(l => l.StationPost);
            return truePostos.OrderBy(o => o.StationPostName).ToList();
        }


        public List<TimeData> getStationTimes(int? id)
        {
            var ttt = db.TimeAtribuitions.Where(o => o.StationPostId == id).Select(l => l.TimeData);
            return ttt.OrderBy(o => o.Time.Hour).ToList();
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

        // GET: Reserves/Create
        [Authorize(Roles = "Client")]
        public ActionResult ReservThisPost(int? id, int? timeId, int? stationId)
        {
            if (!User.IsInRole("Client"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

            ViewBag.RechargeModId = new SelectList(db.RechargeMods, "RechargeModId", "RechargeModName");
            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName");

            ViewBag.PostId = id;
            ViewBag.TimeId = timeId;
            ViewBag.StationId = stationId;

            StationPost stationPost = db.StationPosts.Find(id);
            Station station = db.Stations.Find(stationId);
            if (stationPost == null)
            {
                return HttpNotFound();
            }

            TimeAtribuition time = db.TimeAtribuitions.Where(o => o.TimeDataId == timeId && o.StationPostId == id).Single();
            TimeData timeData = db.TimeDatas.Where(o => o.Status == false && o.Time.Hour == time.TimeData.Time.Hour).Single();

            DateTime reservTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                timeData.Time.Hour,
                timeData.Time.Minute,
                timeData.Time.Second
            );

            string userId = db.Users.Where(o => o.UserName.Equals(User.Identity.Name)).Single().Id;

            Reserve reserve = new Reserve()
            {
                Date = reservTime,
                Completed = 1,
                StationPost = stationPost,
                StationPostId = stationPost.StationPostId,
                UserId = userId,
                Price = stationPost.Price
            };
            return View(reserve);
        }

        // POST: Reserves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReservThisPost([Bind(Include = "ReserveId,UserId,StationPostId,RechargeModId,Date,Price,Completed")] Reserve reserve, int? id, int? timeId, int? stationId)
        {
            if (!User.IsInRole("Client"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

            if (id == null || timeId == null || stationId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StationPost stationPost = db.StationPosts.Find(id);
            Station station = db.Stations.Find(stationId);

            if (stationPost == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                string userId = db.Users.Where(o => o.UserName.Equals(User.Identity.Name)).Single().Id;

                decimal saldo = db.MoneyAtribuitions.Where(o => o.UserId.Equals(userId)).Single().Cash;

                if ((saldo - stationPost.Price) < 0)
                {
                    ModelState.AddModelError(string.Empty, "Não tem dinheiro suficiente na sua conta.");

                    ViewBag.RechargeModId = new SelectList(db.RechargeMods, "RechargeModId", "RechargeModName", reserve.RechargeModId);
                    ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName", reserve.StationPostId);

                    return View(reserve);
                }
                else
                {
                    db.MoneyAtribuitions.Where(o => o.UserId.Equals(userId)).Single().Cash = (saldo - stationPost.Price);
                }

                TimeAtribuition time = db.TimeAtribuitions.Where(o => o.TimeDataId == timeId && o.StationPostId == id).Single();

                TimeData timeData = db.TimeDatas.Where(o => o.Status == false && o.Time.Hour == time.TimeData.Time.Hour).Single();

                db.TimeAtribuitions.Remove(time);

                TimeAtribuition ta = new TimeAtribuition()
                {
                    TimeData = timeData,
                    TimeDataId = timeData.TimeDataId,
                    StationPost = stationPost,
                    StationPostId = stationPost.StationPostId
                };

                db.TimeAtribuitions.Add(ta);

                //necessário porque por alguma razão desconhecida ele não consegue passar esta informação
                reserve.StationPostId = (int) id;
                //---------------------------------------------------------------------------------------

                db.Reserves.Add(reserve);

                db.SaveChanges();
                return RedirectToAction("Details", new { id = station.StationId });
            }

            ViewBag.RechargeModId = new SelectList(db.RechargeMods, "RechargeModId", "RechargeModName", reserve.RechargeModId);
            ViewBag.StationPostId = new SelectList(db.StationPosts, "StationPostId", "StationPostName", reserve.StationPostId);
            return View(reserve);
        }

        // GET: Stations/Create
        [Authorize(Roles = "Owner")]
        public ActionResult Create()
        {
            if (!User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
            if (!User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

            if (NomeEstacaoRepetido(station))
            {
                ModelState.AddModelError("StationName", "Já existe uma estação com este nome.");
            }

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
            if (!User.IsInRole("Owner") && !User.IsInRole("Admin"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
            if (!User.IsInRole("Owner") && !User.IsInRole("Admin"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
        [Authorize(Roles = "Owner")]
        public ActionResult Delete(int? id)
        {
            if (!User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
            if (!User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

            if (UserName == null || UserName.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Station station = db.Stations.Find(id);

            //Eliminação da atribuição do Owner à estação de carregamento
            var user = db.Users.Where(o => o.UserName.Equals(UserName)).Single();
            StationAtribution sa = db.StationAtributions.Where(o => o.UserId.Equals(user.Id) && station.StationId == o.StationId).Single();
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

        // Validação
        [NonAction]
        private bool NomeEstacaoRepetido(Station s)
        {
            foreach (var aux in db.Stations)
            {
                if (aux.StationName.Equals(s.StationName) && aux.LocalId == s.LocalId)
                {
                    return true;
                }
            }
            return false;
        }

        // Validação
        [NonAction]
        private bool NomePostoRepetido(StationPost s)
        {
            foreach (var aux in db.StationPosts)
            {
                if (aux.StationPostName.Equals(s.StationPostName)) // Verificar a estação!
                {
                    return true;
                }
            }
            return false;
        }
    }
}
