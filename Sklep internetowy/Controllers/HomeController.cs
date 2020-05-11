using Ninject;
using Sklep_internetowy.DAL;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Sklep_internetowy.Controllers
{
    public class HomeController : Controller
    {
        
        private IContextServices service;

        public HomeController(IContextServices service)
        {
            this.service = service;
        }
   
        public ActionResult Index()
        {          
            return View(service.GetBestsellersAndNewsForMainPage());
        }

        public ActionResult StaticPages(string name)
        {
            return View(name);
        }

        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {
          
            return PartialView("_MainCategoriesMenu", service.GetAllMainCategories());
        }


    }
}