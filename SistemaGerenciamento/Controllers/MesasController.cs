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
    public class MesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mesas
        public ActionResult Index()
        {
            var mesasOrdenadas = db.Mesas.OrderBy(m => m.Numero).ToList();
            return View(mesasOrdenadas);
        }

        // GET: Mesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // GET: Mesas/Create
        public ActionResult Create()
        {
            // Preenchendo automaticamente o número da mesa
            var nextNumero = db.Mesas.Any() ? db.Mesas.Max(m => m.Numero) + 1 : 1;
            ViewBag.Numero = nextNumero;
            return View();
        }

        // POST: Mesas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Numero,Capacidade")] Mesa mesa)
        {
            // Verifica se a capacidade é maior que zero
            if (mesa.Capacidade <= 0)
            {
                ModelState.AddModelError("Capacidade", "A capacidade deve ser maior que zero.");
            }

            // Verifica se o número já existe
            if (db.Mesas.Any(m => m.Numero == mesa.Numero))
            {
                ModelState.AddModelError("Numero", "Já existe uma mesa com este número.");
            }

            if (ModelState.IsValid)
            {
                db.Mesas.Add(mesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mesa);
        }


        // GET: Mesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // POST: Mesas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numero,Capacidade")] Mesa mesa)
        {
            if (mesa.Capacidade <= 0)
            {
                ModelState.AddModelError("Capacidade", "A capacidade deve ser maior que zero.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(mesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mesa);
        }

        // GET: Mesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mesa mesa = db.Mesas.Find(id);
            db.Mesas.Remove(mesa);
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
