using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sklep_internetowy.App_Start;
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
using System.Threading.Tasks;
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
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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

        [HttpGet]
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

        [HttpGet]
        public async Task<ActionResult> PayForOrder()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var order = new Order()
                {
                    Firstname = user.UserData.Firstname,
                    Lastname = user.UserData.Lastname,
                    Adress = user.UserData.Adress,
                    City = user.UserData.City,
                    ZipCode = user.UserData.ZipCode,
                    PhoneNumber = user.UserData.Phone,
                    Email = user.UserData.Email
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("PayForOrder", "ShoppingCart") });
            }

        }

        [HttpPost]
        public async Task<ActionResult> PayForOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var newOrder = shoppingService.CreateOrder(order, userId);

                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                shoppingService.ClearShoppingCart();

                return RedirectToAction("ConfirmOrder", "ShoppingCart");
            }
            else
            {
                return View(order);
            }
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }

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