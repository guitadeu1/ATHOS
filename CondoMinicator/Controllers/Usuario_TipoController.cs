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
    public class Usuario_TipoController : Controller
    {
        private CondoDBContext db = new CondoDBContext();

        // GET: /Usuario_Tipo/
        public ActionResult Index()
        {
            return View(db.Usuario_Tipos.ToList());
        }

        // GET: /Usuario_Tipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario_Tipo usuario_tipo = db.Usuario_Tipos.Find(id);
            if (usuario_tipo == null)
            {
                return HttpNotFound();
            }
            return View(usuario_tipo);
        }

        // GET: /Usuario_Tipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Usuario_Tipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Tipo")] Usuario_Tipo usuario_tipo)
        {
            if (ModelState.IsValid)
            {
                db.Usuario_Tipos.Add(usuario_tipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario_tipo);
        }

        // GET: /Usuario_Tipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario_Tipo usuario_tipo = db.Usuario_Tipos.Find(id);
            if (usuario_tipo == null)
            {
                return HttpNotFound();
            }
            return View(usuario_tipo);
        }

        // POST: /Usuario_Tipo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Tipo")] Usuario_Tipo usuario_tipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario_tipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario_tipo);
        }

        // GET: /Usuario_Tipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario_Tipo usuario_tipo = db.Usuario_Tipos.Find(id);
            if (usuario_tipo == null)
            {
                return HttpNotFound();
            }
            return View(usuario_tipo);
        }

        // POST: /Usuario_Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario_Tipo usuario_tipo = db.Usuario_Tipos.Find(id);
            db.Usuario_Tipos.Remove(usuario_tipo);
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
