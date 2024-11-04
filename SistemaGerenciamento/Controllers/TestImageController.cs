using SistemaGerenciamento.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGerenciamento.Controllers
{
    public class TestImageController : Controller
    {
        // GET: TestImage
        public ActionResult Index()
        {
            using (DBmodel dBmodel = new DBmodel())
            {
                return View(dBmodel.TBLimages.ToList());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(TBLimage tblimage)
        {
            string fileName = Path.GetFileNameWithoutExtension(tblimage.ImageFile.FileName);
            string extension = Path.GetExtension(tblimage.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            tblimage.Image = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            tblimage.ImageFile.SaveAs(fileName);
            using (DBmodel dBmodel = new DBmodel())
            {
                dBmodel.TBLimages.Add(tblimage);
                dBmodel.SaveChanges();
            }
            ModelState.Clear();

            return View();
        }
    }
}