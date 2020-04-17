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
        private readonly IProductDbContext db;
        private readonly IContextServices service;

        public ProductController(IProductDbContext db, IContextServices service)
        {
            this.db = db;
            this.service = service;
        }

        public ProductController()
        {
            db = new ProductDbContext();
            service = new ContextServices();
        }
        public ActionResult ProductDetails(int id)
        {
            ViewBag.Sizes = service.GetSizesForGivenProduct(id);
            ViewBag.Colors = service.GetColorsForGivenProduct(id);
            return View(service.GetAllDetailsForGivenProductId(id));
        }
       
        [HttpPost]
        public ActionResult GetColorForSelectList(int sizeId, int productId)
        {
            SelectList colorsList = service.GetColorsForGivenSize(sizeId, productId);
            return Json(colorsList);
        }

        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {
            return PartialView("_MainCategoriesMenu", service.GetAllMainCategories());
        }

        [ChildActionOnly]
        public ActionResult CategoriesMenu(int idMainCategory)
        {
            return PartialView("_CategoriesMenu", service.GetCategoriesForGivenMainCategory(idMainCategory));
        }


    }
}