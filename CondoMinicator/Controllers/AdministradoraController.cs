using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CondoMinicator.Models;
using CondoMinicator.DAL;

namespace CondoMinicator.Controllers
{
    public class AdministradoraController : Controller
    {
        private CondoDBContext db = new CondoDBContext();

        // GET: /Administradora/
        public ActionResult Index()
        {
            return View(db.Administradoras.ToList());
        }

        // GET: /Administradora/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administradora administradora = db.Administradoras.Find(id);
            if (administradora == null)
            {
                return HttpNotFound();
            }
            return View(administradora);
        }

        // GET: /Administradora/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Administradora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Nome")] Administradora administradora)
        {
            if (ModelState.IsValid)
            {
                db.Administradoras.Add(administradora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administradora);
        }

        // GET: /Administradora/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administradora administradora = db.Administradoras.Find(id);
            if (administradora == null)
            {
                return HttpNotFound();
            }
            return View(administradora);
        }

        // POST: /Administradora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Nome")] Administradora administradora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administradora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administradora);
        }

        // GET: /Administradora/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administradora administradora = db.Administradoras.Find(id);
            if (administradora == null)
            {
                return HttpNotFound();
            }
            return View(administradora);
        }

        // POST: /Administradora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administradora administradora = db.Administradoras.Find(id);
            db.Administradoras.Remove(administradora);
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
