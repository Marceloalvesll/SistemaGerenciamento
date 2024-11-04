using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Mesa);
            return View(reservas.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Use Include para carregar a mesa relacionada
            Reserva reserva = db.Reservas.Include(r => r.Mesa).SingleOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return HttpNotFound();
            }

            return View(reserva);
        }
        // GET: Reservas/Create
        public ActionResult Create()
        {
            var mesas = db.Mesas.ToList();
            var selectList = mesas.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"Mesa {m.Numero} - Capacidade: {m.Capacidade}"
            }).ToList();

            ViewBag.MesaId = new SelectList(selectList, "Value", "Text");
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DataHoraReserva,QuantidadePessoas,MesaId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var mesa = db.Mesas.Find(reserva.MesaId);
                if (mesa != null)
                {
                    if (reserva.QuantidadePessoas <= 0)
                    {
                        ModelState.AddModelError("QuantidadePessoas", "A quantidade de pessoas deve ser maior que zero.");
                    }
                    else if (reserva.QuantidadePessoas > mesa.Capacidade)
                    {
                        ModelState.AddModelError("QuantidadePessoas", "A quantidade de pessoas excede a capacidade da mesa.");
                    }
                    else if (db.Reservas.Any(r => r.MesaId == reserva.MesaId && r.DataHoraReserva == reserva.DataHoraReserva))
                    {
                        ModelState.AddModelError("DataHoraReserva", "Já existe uma reserva para essa mesa neste horário.");
                    }
                }

                if (ModelState.IsValid)
                {
                    db.Reservas.Add(reserva);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            // Recarregar a lista de mesas com o texto formatado para exibição correta
            var mesas = db.Mesas.ToList();
            var selectList = mesas.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"Mesa {m.Numero} - Capacidade: {m.Capacidade}"
            }).ToList();

            ViewBag.MesaId = new SelectList(selectList, "Value", "Text", reserva.MesaId);
            return View(reserva);
        }


        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }

            var mesas = db.Mesas.ToList();
            var selectList = mesas.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"Mesa {m.Numero} - Capacidade: {m.Capacidade}"
            }).ToList();

            ViewBag.MesaId = new SelectList(selectList, "Value", "Text", reserva.MesaId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DataHoraReserva,QuantidadePessoas,MesaId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var mesa = db.Mesas.Find(reserva.MesaId);
                if (mesa != null)
                {
                    if (reserva.QuantidadePessoas <= 0)
                    {
                        ModelState.AddModelError("QuantidadePessoas", "A quantidade de pessoas deve ser maior que zero.");
                    }
                    else if (reserva.QuantidadePessoas > mesa.Capacidade)
                    {
                        ModelState.AddModelError("QuantidadePessoas", "A quantidade de pessoas excede a capacidade da mesa.");
                    }
                    else if (db.Reservas.Any(r => r.MesaId == reserva.MesaId && r.DataHoraReserva == reserva.DataHoraReserva && r.Id != reserva.Id))
                    {
                        ModelState.AddModelError("DataHoraReserva", "Já existe uma reserva para essa mesa neste horário.");
                    }
                }

                if (ModelState.IsValid)
                {
                    db.Entry(reserva).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            // Recarregar a lista de mesas com o texto formatado para exibição correta
            var mesas = db.Mesas.ToList();
            var selectList = mesas.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"Mesa {m.Numero} - Capacidade: {m.Capacidade}"
            }).ToList();

            ViewBag.MesaId = new SelectList(selectList, "Value", "Text", reserva.MesaId);
            return View(reserva);
        }


        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Incluindo a Mesa relacionada ao carregar a reserva
            Reserva reserva = db.Reservas.Include(r => r.Mesa).SingleOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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
