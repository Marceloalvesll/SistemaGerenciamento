using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriaDoMenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoriaDoMenus
        public ActionResult Index()
        {
            return View(db.CategoriaDoMenus.ToList());
        }

        // GET: CategoriaDoMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaDoMenu categoriaDoMenu = db.CategoriaDoMenus.Find(id);
            if (categoriaDoMenu == null)
            {
                return HttpNotFound();
            }
            return View(categoriaDoMenu);
        }

        // GET: CategoriaDoMenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaDoMenus/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome")] CategoriaDoMenu categoriaDoMenu)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaDoMenus.Add(categoriaDoMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaDoMenu);
        }

        // GET: CategoriaDoMenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaDoMenu categoriaDoMenu = db.CategoriaDoMenus.Find(id);
            if (categoriaDoMenu == null)
            {
                return HttpNotFound();
            }
            return View(categoriaDoMenu);
        }

        // POST: CategoriaDoMenus/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] CategoriaDoMenu categoriaDoMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaDoMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaDoMenu);
        }

        // GET: CategoriaDoMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaDoMenu categoriaDoMenu = db.CategoriaDoMenus.Find(id);
            if (categoriaDoMenu == null)
            {
                return HttpNotFound();
            }
            return View(categoriaDoMenu);
        }

        // POST: CategoriaDoMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaDoMenu categoriaDoMenu = db.CategoriaDoMenus.Find(id);
            db.CategoriaDoMenus.Remove(categoriaDoMenu);
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
