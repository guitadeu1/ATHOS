using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CondoMinicator.Models;
using CondoMinicator.DAL;
using System.IO;

namespace CondoMinicator.Controllers
{
    public class ComunicadoController : Controller
    {
        private CondoDBContext db = new CondoDBContext();

        // GET: /Comunicado/
        public ActionResult Index()
        {
            var comunicados = db.Comunicados.Include(c => c.Assunto).Include(c => c.Condominio).Include(c => c.De).Include(c => c.Para);
            return View(comunicados.ToList());
        }

        // GET: /Comunicado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunicado comunicado = db.Comunicados.Find(id);
            if (comunicado == null)
            {
                return HttpNotFound();
            }
            return View(comunicado);
        }

        // GET: /Comunicado/Create
        public ActionResult Create()
        {
            ViewBag.Usuario_de_ID = new SelectList(db.Usuarios, "ID", "Nome");
            ViewBag.Usuario_para_ID = new SelectList(db.Usuarios, "ID", "Nome");
            ViewBag.AssuntoID = new SelectList(db.Assuntos, "ID", "Tipo");
            ViewBag.CondominioID = new SelectList(db.Administradoras, "ID", "Nome");
            return View();
        }

        // POST: /Comunicado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Usuario_de_ID,Usuario_para_ID,CondominioID,AssuntoID,Mensagem")] Comunicado comunicado)
        {
            if (ModelState.IsValid)
            {
                EnviarEmail(comunicado);
                db.Comunicados.Add(comunicado);
                db.SaveChanges();
                return RedirectToAction("../Home/AddComunicado", new RouteValueDictionary(new { msg = "Comunicado enviado com sucesso!" }));
            }

            ViewBag.AssuntoID = new SelectList(db.Assuntos, "ID", "Tipo", comunicado.AssuntoID);
            ViewBag.CondominioID = new SelectList(db.Administradoras, "ID", "Nome", comunicado.CondominioID);
            ViewBag.Usuario_de_ID = new SelectList(db.Usuarios, "ID", "Nome", comunicado.Usuario_de_ID);
            ViewBag.Usuario_para_ID = new SelectList(db.Usuarios, "ID", "Nome", comunicado.Usuario_para_ID);
            return View(comunicado);
        }

        private void EnviarEmail(Comunicado comunicado)
        {
            string De = db.Usuarios.FirstOrDefault(u => u.ID == comunicado.Usuario_de_ID).Nome;
            string Para = db.Usuarios.FirstOrDefault(u => u.ID == comunicado.Usuario_para_ID).Nome;
            string Assunto = db.Assuntos.FirstOrDefault(u => u.ID == comunicado.AssuntoID).Tipo;

            Log("Comunicado enviado com sucesso em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            Log("De: " + De);
            Log("Para: " + Para);
            Log("Assunto: " + Assunto);
            Log("Mensagem: " + comunicado.Mensagem);
            Log("-------------------------------------------");
        }

        public void Log(string line)
        {
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            StreamWriter sw = new StreamWriter(path + @"\log.txt", true);
            sw.WriteLine(line);  
            sw.Close();
        }

        // GET: /Comunicado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunicado comunicado = db.Comunicados.Find(id);
            if (comunicado == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssuntoID = new SelectList(db.Assuntos, "ID", "Tipo", comunicado.AssuntoID);
            ViewBag.CondominioID = new SelectList(db.Administradoras, "ID", "Nome", comunicado.CondominioID);
            ViewBag.Usuario_de_ID = new SelectList(db.Usuarios, "ID", "Nome", comunicado.Usuario_de_ID);
            ViewBag.Usuario_para_ID = new SelectList(db.Usuarios, "ID", "Nome", comunicado.Usuario_para_ID);
            return View(comunicado);
        }

        // POST: /Comunicado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Usuario_de_ID,Usuario_para_ID,CondominioID,AssuntoID,Mensagem")] Comunicado comunicado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comunicado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssuntoID = new SelectList(db.Assuntos, "ID", "Tipo", comunicado.AssuntoID);
            ViewBag.CondominioID = new SelectList(db.Administradoras, "ID", "Nome", comunicado.CondominioID);
            ViewBag.Usuario_de_ID = new SelectList(db.Usuarios, "ID", "Nome", comunicado.Usuario_de_ID);
            ViewBag.Usuario_para_ID = new SelectList(db.Usuarios, "ID", "Nome", comunicado.Usuario_para_ID);
            return View(comunicado);
        }

        // GET: /Comunicado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunicado comunicado = db.Comunicados.Find(id);
            if (comunicado == null)
            {
                return HttpNotFound();
            }
            return View(comunicado);
        }

        // POST: /Comunicado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comunicado comunicado = db.Comunicados.Find(id);
            db.Comunicados.Remove(comunicado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetCondominioUsuario(int Usuario_de_ID)
        {
            if (Usuario_de_ID > 0)
            {

                int CondominioID = db.Usuarios.AsNoTracking()
                        .FirstOrDefault(n => n.ID == Usuario_de_ID).CondominioID;

                return Json(CondominioID, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetUsuariosPorAssuntoCondominio(int AssuntoID, int CondominioID)
        {
            if (AssuntoID > 0 && CondominioID > 0)
            {
                var tipos_condo = new int[] { 2, 4 };
                IEnumerable<SelectListItem> usuarios;

                if (AssuntoID == 1) // Administrativo
                {
                    usuarios = db.Usuarios.AsNoTracking()
                                            .OrderBy(u => u.Nome)
                                            .Where(u => u.CondominioID == CondominioID && u.Usuario_TipoID == 3)
                                            .Select(u =>
                                               new SelectListItem
                                               {
                                                   Value = u.ID.ToString(),
                                                   Text = u.Nome
                                               }).ToList();

                } else { // Condominial
                    usuarios = db.Usuarios.AsNoTracking()
                        .OrderBy(u => u.Usuario_TipoID)
                        .Where(u => u.CondominioID == CondominioID && tipos_condo.Contains(u.Usuario_TipoID))
                        .Select(u =>
                           new SelectListItem
                           {
                               Value = u.ID.ToString(),
                               Text = u.Nome
                           }).ToList();
                }

                //return new SelectList(usuarios, "Value", "Text");
                return Json(usuarios, JsonRequestBehavior.AllowGet);
            }
            return null;
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
