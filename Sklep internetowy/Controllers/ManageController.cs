using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Sklep_internetowy.App_Start;
using Sklep_internetowy.DAL;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sklep_internetowy.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IProductDbContext db;
        private IContextServices service;

        public ManageController(IProductDbContext db, IContextServices service)
        {
            this.db = db;
            this.service = service;
        }


        public enum ManageMessageId
        {
            Success,
            Error
        }

        private ApplicationUserManager _userManager;
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
            {
                ViewBag.UserIsAdmin = true;
            }
            else
            {
                ViewBag.UserIsAdmin = false;
            }

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialViewModel
            {
                Message = message,
                UserData = user.UserData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.Success });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.Success;

            return RedirectToAction("Index", new { Message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")]UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = userData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public async Task<ActionResult> OrdersHistory()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.isAdmin = isAdmin;

            if (user == null)
            {
                return View("Error");
            }

            List<Order> orders;

            if (isAdmin)
            {
                orders = db.Orders.Include("OrderPositions").OrderByDescending(o => o.AddingDate).ToList();
            }
            else
            {
                orders = db.Orders.Where(o => o.UserId == user.Id).Include("OrderPositions").OrderByDescending(o => o.AddingDate).ToList();
            }

            return View(orders);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangeStateOfOrder(int orderId, StateOfOrder enumState)
        {
            Order updateOrder = db.Orders.Find(orderId);
            if (enumState == StateOfOrder.Completed)
            {
                updateOrder.StateOfOrder = StateOfOrder.InProgress;
            }
            else
            {
                updateOrder.StateOfOrder = StateOfOrder.Completed;
            }

            db.SaveChangesWrapped();
            return RedirectToAction("OrdersHistory");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(int? productVariantId, bool? confirm)
        {
            ProductVariant productVariant;

            if (productVariantId.HasValue)
            {
                ViewBag.EditMode = true;
                productVariant = db.ProductsVariant.Find(productVariantId);
            }
            else
            {
                ViewBag.EditMode = false;
                productVariant = new ProductVariant();
            }

            var result = new EditProductViewModel();
            result.Product = db.Products.ToList();
            result.Size = db.Sizes.ToList();
            result.Color = db.Colors.ToList();
            result.ProductVariant = productVariant;
            result.Confirm = confirm;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(EditProductViewModel model)
        {
            db.ProductsVariant.Add(model.ProductVariant);
            db.SaveChangesWrapped();
            return RedirectToAction("AddProduct", new { confirm = true });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddNewProduct(int? productId, bool? confirm)
        {
            Product product;

            if (productId.HasValue)
            {
                ViewBag.EditModel = true;
                product = db.Products.Find(productId);
            }
            else
            {
                ViewBag.EditModel = false;
                product = new Product();
            }

            var result = new EditProductNewViewModel();
            result.Product = product;
            result.Category = db.Categories.ToList();
            result.Brand = db.Brands.ToList();
            result.Gender = db.Genders.ToList();
            result.Confirm = confirm;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddNewProduct(EditProductNewViewModel model, HttpPostedFileBase file)
        {
            if (model.Product.ProductId > 0)
            {
                Product product = db.Products.Find(model.Product.ProductId);
                product.Name = model.Product.Name;
                product.Description = model.Product.Description;
                product.Price = model.Product.Price;
                product.isBestseller = model.Product.isBestseller;
                product.isAvaliable = model.Product.isAvaliable;
                product.isSize = model.Product.isSize;
                product.isColor = model.Product.isColor;
                product.CategoryId = model.Product.CategoryId;
                product.BrandId = model.Product.BrandId;
                product.GenderId = model.Product.GenderId;

                db.SaveChangesWrapped();
                return RedirectToAction("AddNewProduct", new { confirm = true });
            }
            else
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;

                        var path = Path.Combine(Server.MapPath(AppConfig.GetGraphicFileRelativePath), filename);
                        file.SaveAs(path);

                        model.Product.GraphicFileName = filename;
                        model.Product.AddingDate = DateTime.Now;

                        db.Products.Add(model.Product);
                        db.SaveChangesWrapped();
                        return RedirectToAction("AddProduct", new { confirm = true });
                    }
                    else
                    {
                        var categories = db.Categories.ToList();
                        var genders = db.Genders.ToList();
                        var brands = db.Brands.ToList();
                        model.Category = categories;
                        model.Brand = brands;
                        model.Gender = genders;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku!");
                    var categories = db.Categories.ToList();
                    var genders = db.Genders.ToList();
                    var brands = db.Brands.ToList();
                    model.Category = categories;
                    model.Brand = brands;
                    model.Gender = genders;
                    return View(model);
                }
            }
        }





    }
}