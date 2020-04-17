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
        private IContextServices service;
        private IShoppingCartServices shoppingService;
        
        public ShoppingCartController()
        {
            db = new ProductDbContext();
            session = new SessionManager();
            service = new ContextServices();
            shoppingService = new ShoppingCartServices();           
        }

        public ShoppingCartController(IProductDbContext db, ISessionManager session, IShoppingCartServices shoppingService, IContextServices service)
        {
            this.db = db;
            this.session = session;
            this.service = service;
            this.shoppingService = shoppingService;
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
            var productVariantId = shoppingService.GetIdAddedProductVariantToShoppingCart(sizeId, colorId, productId);
            shoppingService.Add(productVariantId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromShoppingCart(int productVariantId)
        {
            int removePositionQuantity = shoppingService.Remove(productVariantId);
            int shoppingCartPositionsQuantity = shoppingService.GetCountOfShoppingCartPositions();
            decimal shoppingCartTotalPrice = shoppingService.GetValueOfShoppingCart();

            var result = new ShoppingCartRemoveViewModel() { RemovePositionId = productVariantId, RemovePositionQuantity = removePositionQuantity, ShoppingCartPositionsQuantity = shoppingCartPositionsQuantity, ShoppingCartTotalPrice = shoppingCartTotalPrice };
            return Json(result);
        }

        //należy przetestować
        public int GetQuantityPositionsOfShoppingCart()
        {
            return shoppingService.GetCountOfShoppingCartPositions();
        }

        [ChildActionOnly]
        public ActionResult MainCategoriesMenu()
        {
            return PartialView("_MainCategoriesMenu", service.GetAllMainCategories());
        }
    }
}