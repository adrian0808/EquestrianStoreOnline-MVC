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
    public class HomeController : Controller
    {
        private IProductDbContext db;
        private IContextServices cs;

        public HomeController(IProductDbContext _db, IContextServices _cs)
        {
            db = _db;
            cs = _cs;
        }

        public HomeController()
        {
            db = new ProductDbContext();
            cs = new ContextServices();
        }

        public ActionResult Index()
        {      
            return View(cs.GetBestsellersAndNewsForMainPage());
        }

        public ActionResult StaticPages(string name)
        {
            return View(name);
        }

        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {      
            return PartialView("_MainCategoriesMenu", cs.GetAllMainCategories());
        }


    }
}