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
    public class CategoryController : Controller
    {
        IProductDbContext db;
        IContextServices service;

        public CategoryController(IProductDbContext db, IContextServices service)
        {
            this.db = db;
            this.service = service;
        }

        public CategoryController()
        {
            db = new ProductDbContext();
            service = new ContextServices();
        }

        public ActionResult MainCategoryContent(int idMainCategory, string searchTerm)
        {

            return View(service.GetProductsForGivenMainCategoryWithFilter(idMainCategory, searchTerm));
        }

        public ActionResult CategoryContent(int idCategory, string searchTerm)
        {
            return View(service.GetProductsForGivenCategoryWithFilter(idCategory, searchTerm));
        }

        public ActionResult BestsellerContent()
        {
            return View(service.GetProductsWhichAreBestsellers());
        }

        public ActionResult NewsContent()
        {
            return View(service.GetProductsWhichAreNew());
        }

        [ChildActionOnly]
        public ActionResult CategoriesMenu(int idMainCategory)
        {
            return PartialView("_CategoriesMenu", service.GetCategoriesForGivenMainCategory(idMainCategory));
        }

        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {
            return PartialView("_MainCategoriesMenu", service.GetAllMainCategories());
        }
    }

}