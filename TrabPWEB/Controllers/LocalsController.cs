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
    public class LocalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locals
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var locals = db.Locals.Include(l => l.Region).OrderBy(k => k.Region.RegionName).ThenBy(l => l.LocalName);
            return View(locals.ToList());
        }

        // GET: Locals/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET: Locals/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "RegionName");
            return View();
        }

        // POST: Locals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocalId,LocalName,RegionId")] Local local)
        {
            if (NomeLocalRepetido(local))
            {
                ModelState.AddModelError("LocalName", "Este nome já existe nesta região.");
            }

            if (ModelState.IsValid)
            {
                db.Locals.Add(local);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "RegionName", local.RegionId);
            return View(local);
        }

        // GET: Locals/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "RegionName", local.RegionId);
            return View(local);
        }

        // POST: Locals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocalId,LocalName,RegionId")] Local local)
        {
            if (NomeLocalRepetido(local))
            {
                ModelState.AddModelError("LocalName", "Este nome já existe nesta região.");
            }

            if (ModelState.IsValid)
            {
                db.Locals.Where(o => o.LocalId == local.LocalId).Single().LocalName = local.LocalName;
                db.Locals.Where(o => o.LocalId == local.LocalId).Single().Region = local.Region;
                db.Locals.Where(o => o.LocalId == local.LocalId).Single().RegionId = local.RegionId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "RegionName", local.RegionId);
            return View(local);
        }

        // GET: Locals/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (db.Stations.Where(p => p.LocalId == id).Any())
            {
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: Locals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (db.Stations.Where(p => p.LocalId == id).Any())
            {
                return RedirectToAction("Index");
            }

            Local local = db.Locals.Find(id);
            db.Locals.Remove(local);
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
        private bool NomeLocalRepetido(Local l)
        {
            foreach(var aux in db.Locals)
            {
                if(aux.LocalName.Equals(l.LocalName) && aux.RegionId == l.RegionId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
