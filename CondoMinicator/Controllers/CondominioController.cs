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
    public class CondominioController : Controller
    {
        private CondoDBContext db = new CondoDBContext();

        // GET: /Condominio/
        public ActionResult Index()
        {
            var condominios = db.Condominios.Include(c => c.Administradora).Include(c => c.Responsavel);
            return View(condominios.ToList());
        }

        // GET: /Condominio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condominio condominio = db.Condominios.Find(id);
            if (condominio == null)
            {
                return HttpNotFound();
            }
            return View(condominio);
        }

        // GET: /Condominio/Create
        public ActionResult Create()
        {
            ViewBag.AdministradoraID = new SelectList(db.Administradoras, "ID", "Nome");
            ViewBag.Usuario_TipoID = new SelectList(db.Usuario_Tipos, "ID", "Tipo");
            return View();
        }

        // POST: /Condominio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Nome,AdministradoraID,Usuario_TipoID")] Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                db.Condominios.Add(condominio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdministradoraID = new SelectList(db.Administradoras, "ID", "Nome", condominio.AdministradoraID);
            ViewBag.Usuario_TipoID = new SelectList(db.Usuario_Tipos, "ID", "Tipo", condominio.Usuario_TipoID);
            return View(condominio);
        }

        // GET: /Condominio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condominio condominio = db.Condominios.Find(id);
            if (condominio == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdministradoraID = new SelectList(db.Administradoras, "ID", "Nome", condominio.AdministradoraID);
            ViewBag.Usuario_TipoID = new SelectList(db.Usuario_Tipos, "ID", "Tipo", condominio.Usuario_TipoID);
            return View(condominio);
        }

        // POST: /Condominio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Nome,AdministradoraID,Usuario_TipoID")] Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condominio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdministradoraID = new SelectList(db.Administradoras, "ID", "Nome", condominio.AdministradoraID);
            ViewBag.Usuario_TipoID = new SelectList(db.Usuario_Tipos, "ID", "Tipo", condominio.Usuario_TipoID);
            return View(condominio);
        }

        // GET: /Condominio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condominio condominio = db.Condominios.Find(id);
            if (condominio == null)
            {
                return HttpNotFound();
            }
            return View(condominio);
        }

        // POST: /Condominio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Condominio condominio = db.Condominios.Find(id);
            db.Condominios.Remove(condominio);
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
