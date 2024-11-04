using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.Controllers
{
    [Authorize]
    public class PrePedidoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrePedidoes
        public ActionResult Index()
        {
            var prePedidos = db.PrePedidos.Include(p => p.PrePedidoItens.Select(i => i.ItemDoMenu));
            return View(prePedidos.ToList());
        }

        public ActionResult Details(int? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }

    PrePedido prePedido = db.PrePedidos
        .Include(p => p.PrePedidoItens.Select(i => i.ItemDoMenu)) // Carregando os itens do menu
        .FirstOrDefault(p => p.Id == id);

    if (prePedido == null)
    {
        return HttpNotFound();
    }

    return View(prePedido);
}

        // GET: PrePedidoes/Create
        public ActionResult Create()
        {
            var itens = db.ItensDoMenu
                .Where(i => i.Disponivel)
                .Select(i => new ItemDoMenuViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Preco = i.Preco,
                    Image = i.Image
                })
                .ToList();

            ViewBag.ItensDoMenu = itens;
            return View();
        }
       
        // POST: PrePedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] PrePedido prePedido, List<int> itensSelecionados)
        {
            if (ModelState.IsValid)
            {
                if (itensSelecionados != null && itensSelecionados.Count > 0)
                {
                    foreach (var itemId in itensSelecionados)
                    {
                        prePedido.PrePedidoItens.Add(new PrePedidoItemDoMenu { ItemDoMenuId = itemId });
                    }

                    prePedido.Total = db.ItensDoMenu.Where(item => itensSelecionados.Contains(item.Id)).Sum(item => item.Preco);
                    prePedido.Status = "Pendente";

                    db.PrePedidos.Add(prePedido);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ItensSelecionados", "Você deve selecionar pelo menos um item."); // Adiciona erro ao ModelState
                }
            }

            ViewBag.ItensDoMenu = new SelectList(db.ItensDoMenu.Where(i => i.Disponivel), "Id", "Nome", itensSelecionados);
            return View(prePedido);
        }


        // GET: PrePedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PrePedido prePedido = db.PrePedidos
                .Include(p => p.PrePedidoItens.Select(i => i.ItemDoMenu))
                .FirstOrDefault(p => p.Id == id);

            if (prePedido == null)
            {
                return HttpNotFound();
            }

            // Carrega a lista de itens do menu para exibir na view
            var itens = db.ItensDoMenu
                .Where(i => i.Disponivel)
                .Select(i => new ItemDoMenuViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Preco = i.Preco,
                    Image = i.Image
                })
                .ToList();

            ViewBag.ItensDoMenu = itens; // Passa a lista de itens do menu para a view
            ViewBag.ItensSelecionados = prePedido.PrePedidoItens.Select(i => i.ItemDoMenuId).ToList(); // Lista de itens já selecionados

            return View(prePedido);
        }

        // POST: PrePedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] PrePedido prePedido, List<int> itensSelecionados)
        {
            if (ModelState.IsValid)
            {
                var existingPrePedido = db.PrePedidos
                    .Include(p => p.PrePedidoItens)
                    .FirstOrDefault(p => p.Id == prePedido.Id);

                if (existingPrePedido != null)
                {
                    // Remover itens que foram desmarcados
                    foreach (var item in existingPrePedido.PrePedidoItens.ToList())
                    {
                        if (itensSelecionados == null || !itensSelecionados.Contains(item.ItemDoMenuId))
                        {
                            db.PrePedidoItens.Remove(item);
                        }
                    }

                    // Adicionar novos itens que foram selecionados
                    if (itensSelecionados != null)
                    {
                        foreach (var itemId in itensSelecionados)
                        {
                            if (!existingPrePedido.PrePedidoItens.Any(i => i.ItemDoMenuId == itemId))
                            {
                                existingPrePedido.PrePedidoItens.Add(new PrePedidoItemDoMenu { ItemDoMenuId = itemId, PrePedidoId = existingPrePedido.Id });
                            }
                        }
                    }

                    // Atualizar o total com base nos itens selecionados, se houver algum
                    existingPrePedido.Total = itensSelecionados != null
                        ? db.ItensDoMenu.Where(item => itensSelecionados.Contains(item.Id)).Sum(item => item.Preco)
                        : 0;

                    // Salvar alterações
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            // Recarregar os itens do menu e os itens selecionados em caso de erro
            var itens = db.ItensDoMenu
                .Where(i => i.Disponivel)
                .Select(i => new ItemDoMenuViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Preco = i.Preco,
                    Image = i.Image
                })
                .ToList();

            ViewBag.ItensDoMenu = itens;
            ViewBag.ItensSelecionados = itensSelecionados ?? new List<int>();

            return View(prePedido);
        }



        // GET: PrePedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrePedido prePedido = db.PrePedidos.Include(p => p.PrePedidoItens.Select(i => i.ItemDoMenu)).FirstOrDefault(p => p.Id == id);
            if (prePedido == null)
            {
                return HttpNotFound();
            }
            return View(prePedido);
        }

        // POST: PrePedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrePedido prePedido = db.PrePedidos.Find(id);
            db.PrePedidos.Remove(prePedido);
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
