using Sklep_internetowy.DAL;
using Sklep_internetowy.Models;
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
        IContextServices cs;

        public CategoryController(IProductDbContext db_, IContextServices cs_)
        {
            db = db_;
            cs = cs_;
        }

        public CategoryController()
        {
            db = new ProductDbContext();
            cs = new ContextServices();
        }

        public ActionResult MainCategoryContent(int idMainCategory, string searchTerm)
        {

            return View(cs.GetProductsForGivenMainCategoryWithFilter(idMainCategory, searchTerm));
        }

        public ActionResult CategoryContent(int idCategory)
        {
            return View(cs.GetProductsForGivenCategory(idCategory));
        }

        public ActionResult BestsellerContent()
        {
            return View(cs.GetProductsWhichAreBestsellers());
        }



        [ChildActionOnly]
        public ActionResult CategoriesMenu(int idMainCategory)
        {
            return PartialView("_CategoriesMenu", cs.GetCategoriesForGivenMainCategory(idMainCategory));
        }

        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {
            return PartialView("_MainCategoriesMenu", cs.GetAllMainCategories());
        }
    }

}