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
            return View(cs.GetAllDetailsForGivenProductId(id));
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