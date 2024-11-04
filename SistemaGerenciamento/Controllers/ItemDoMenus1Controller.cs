

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemDoMenus1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemDoMenus1
        public ActionResult Index()
        {
            var itensDoMenu = db.ItensDoMenu.Include(i => i.Categoria);
            return View(itensDoMenu.ToList());
        }

        // GET: ItemDoMenus1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Inclui o relacionamento com Categoria ao buscar o item
            var itemDoMenu = db.ItensDoMenu.Include(i => i.Categoria).FirstOrDefault(i => i.Id == id);
            if (itemDoMenu == null)
            {
                return HttpNotFound();
            }

            return View(itemDoMenu);
        }

        // GET: ItemDoMenus1/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.CategoriaDoMenus, "Id", "Nome");
            return View();
        }

        // POST: ItemDoMenus1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Preco,Disponivel,CategoriaId,ImageFile")] ItemDoMenu itemDoMenu)
        {
            if (ModelState.IsValid)
            {
                if (itemDoMenu.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(itemDoMenu.ImageFile.FileName);
                    string extension = Path.GetExtension(itemDoMenu.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    itemDoMenu.Image = "/image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/image/"), fileName);

                    // Salva a imagem no diretório
                    itemDoMenu.ImageFile.SaveAs(fileName);
                }

                db.ItensDoMenu.Add(itemDoMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.CategoriaDoMenus, "Id", "Nome", itemDoMenu.CategoriaId);
            return View(itemDoMenu);
        }

        // GET: ItemDoMenus1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDoMenu itemDoMenu = db.ItensDoMenu.Find(id);
            if (itemDoMenu == null)
            {
                return HttpNotFound();
            }

            // Preenche PrecoString com o valor atual de Preco
            itemDoMenu.PrecoString = itemDoMenu.Preco.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);

            // Passa a lista de categorias como SelectList para a view
            ViewBag.CategoriaId = new SelectList(db.CategoriaDoMenus, "Id", "Nome", itemDoMenu.CategoriaId);

            return View(itemDoMenu);
        }

        // POST: ItemDoMenus1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,PrecoString,Disponivel,CategoriaId,Image,ImageFile")] ItemDoMenu itemDoMenu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verifica se `PrecoString` não é nulo ou vazio antes de converter
                    if (!string.IsNullOrEmpty(itemDoMenu.PrecoString))
                    {
                        itemDoMenu.Preco = decimal.Parse(itemDoMenu.PrecoString, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        // Define um valor padrão ou gera um erro de validação se o campo estiver vazio
                        ModelState.AddModelError("PrecoString", "O campo Preço é obrigatório.");
                        ViewBag.CategoriaId = new SelectList(db.CategoriaDoMenus, "Id", "Nome", itemDoMenu.CategoriaId);
                        return View(itemDoMenu);
                    }
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("PrecoString", "Formato inválido. Use ponto como separador decimal.");
                    ViewBag.CategoriaId = new SelectList(db.CategoriaDoMenus, "Id", "Nome", itemDoMenu.CategoriaId);
                    return View(itemDoMenu);
                }

                // Processamento da imagem, se aplicável
                if (itemDoMenu.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(itemDoMenu.ImageFile.FileName);
                    string extension = Path.GetExtension(itemDoMenu.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    itemDoMenu.Image = "/image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
                    itemDoMenu.ImageFile.SaveAs(fileName);
                }

                db.Entry(itemDoMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Passa a lista de categorias novamente se houver erro na submissão do formulário
            ViewBag.CategoriaId = new SelectList(db.CategoriaDoMenus, "Id", "Nome", itemDoMenu.CategoriaId);
            return View(itemDoMenu);
        }

        // GET: ItemDoMenus1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDoMenu itemDoMenu = db.ItensDoMenu.Find(id);
            if (itemDoMenu == null)
            {
                return HttpNotFound();
            }
            return View(itemDoMenu);
        }

        // POST: ItemDoMenus1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemDoMenu itemDoMenu = db.ItensDoMenu.Find(id);
            db.ItensDoMenu.Remove(itemDoMenu);
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
