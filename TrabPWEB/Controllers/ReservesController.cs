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
    [Authorize]
    public class ReservesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reserves
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

            var reserves = db.Reserves.Include(r => r.RechargeMod).Include(r => r.StationPost);
            if (User.IsInRole("Client"))
            {
                //Reservas do Client autenticado
                string userId = db.Users.Where(o => o.UserName.Equals(User.Identity.Name)).Select(o => o.Id).Single();
                return View(reserves.Where(o => o.UserId.Equals(userId)).ToList());
                //------------------------------
            }
            else if (User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }
            //Admin e o resto dos utilizadores que não sejam Owner e Client têm acesso a visualizar as reservas feitas
            return View(reserves.ToList());
            //--------------------------------------------------------------------------------------------------------
        }

        // GET: Reserves/Details/5
        public ActionResult Details(int? id)
        {
            if (User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
            if (User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
        public ActionResult Edit([Bind(Include = "ReserveId,UserId,StationPostId,RechargeModId,Date,Price,Completed")] Reserve reserve)
        {
            if (User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
            if (User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

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
            if (User.IsInRole("Owner"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }

            //Dados Auxilixares
            Reserve reserve = db.Reserves.Find(id);
            int stationId = db.StationPostsAtribuition.Where(o => o.StationPostId == reserve.StationPostId).Select(o => o.StationId).Single();
            string userId = db.StationAtributions.Where(o => o.StationId == stationId).Select(o => o.UserId).Single();
            //-----------------

            //Execução do pleno de negocios
            db.MoneyAtribuitions.Where(o => o.UserId.Equals(userId)).Single().Cash += (reserve.Price / 2);
            db.MoneyAtribuitions.Where(o => o.UserId.Equals(reserve.UserId)).Single().Cash += (reserve.Price / 2);
            //-----------------------------

            //Cancelamento da reserva
            reserve.Completed = 0;
            //-----------------------

            //Reatribuição do horário livre ao Posto de Carregamento
            TimeAtribuition time = db.TimeAtribuitions.Where(o => o.TimeData.Time.Hour == reserve.Date.Hour && o.StationPostId == reserve.StationPostId).Single();

            TimeData timeData = db.TimeDatas.Where(o => o.Status == true && o.Time.Hour == time.TimeData.Time.Hour).Single();

            db.TimeAtribuitions.Remove(time);

            TimeAtribuition ta = new TimeAtribuition()
            {
                TimeData = timeData,
                TimeDataId = timeData.TimeDataId,
                StationPost = reserve.StationPost,
                StationPostId = reserve.StationPost.StationPostId
            };

            db.TimeAtribuitions.Add(ta);
            //------------------------------------------------------

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
