using Sklep_internetowy.DAL;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep_internetowy.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductDbContext db;
        private ISessionManager session;
        private IShoppingCartServices shoppingService;
        private IContextServices cs;

        public ShoppingCartController()
        {
            db = new ProductDbContext();
            session = new SessionManager();
            shoppingService = new ShoppingCartServices(db, session, new SystemClock());
            cs = new ContextServices();
        }

        public ShoppingCartController(IProductDbContext db, ISessionManager session, IShoppingCartServices shoppingService, IContextServices cs)
        {
            this.db = db;
            this.session = session;
            this.shoppingService = shoppingService;
            this.cs = cs;
        }

        public ActionResult Index()
        {
            List<ShoppingCartPosition> shoppingCartPositions = shoppingService.GetShoppingCart();
            decimal totalValue = shoppingService.GetValueOfShoppingCart();
            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel() { ShoppingCartPositions = shoppingCartPositions, TotalPrice = totalValue };
            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public ActionResult AddToShoppingCart(int sizeId, int colorId, int productId)
        {
            var productVariantId = db.ProductsVariant.Where(p => p.SizeId == sizeId && p.ColorId == colorId && p.ProductId == productId).Select(i => i.ProductVariantId).SingleOrDefault();
            shoppingService.Add(productVariantId);
            return RedirectToAction("Index");
        }

       
        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {
            return PartialView("_MainCategoriesMenu", cs.GetAllMainCategories());
        }
    }
}