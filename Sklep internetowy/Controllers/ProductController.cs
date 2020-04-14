using Sklep_internetowy.DAL;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep_internetowy.Controllers
{
    public class ProductController : Controller
    {
        IProductDbContext db;
        IContextServices cs;

        public ProductController(IProductDbContext db_, IContextServices cs_)
        {
            db = db_;
            cs = cs_;
        }

        public ProductController()
        {
            db = new ProductDbContext();
            cs = new ContextServices();
        }
        public ActionResult ProductDetails(int id)
        {
            ViewBag.Sizes = new SelectList(db.ProductsVariant.Where(p => p.ProductId == id).Select(p => p.Size), "SizeId", "size");
            ViewBag.Colors = new SelectList(db.ProductsVariant.Where(p => p.ProductId == id).Select(p => p.Color), "ColorId", "color");
            return View(cs.GetAllDetailsForGivenProductId(id));
        }
        public JsonResult GetColors(int id)
        {
            return Json(new SelectList(db.ProductsVariant.Where(p => p.SizeId == id).Select(p => p.Color), "ColorId", "color"));
        }

        //Action result for ajax call
        [HttpPost]
        public ActionResult GetColorByChoosedSize(int sizeId, int productId)
        {
            SelectList colorsList = new SelectList(db.ProductsVariant.Where(p => p.SizeId == sizeId && p.Product.ProductId == productId).Select(c => c.Color).ToList(), "ColorId", "color");
            return Json(colorsList);
        }


        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {
            return PartialView("_MainCategoriesMenu", cs.GetAllMainCategories());
        }

        [ChildActionOnly]
        public ActionResult CategoriesMenu(int idMainCategory)
        {
            return PartialView("_CategoriesMenu", cs.GetCategoriesForGivenMainCategory(idMainCategory));
        }


    }
}