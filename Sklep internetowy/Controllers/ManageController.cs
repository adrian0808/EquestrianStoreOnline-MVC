using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Sklep_internetowy.App_Start;
using Sklep_internetowy.DAL;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sklep_internetowy.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ProductDbContext db;
        private IContextServices service;

        public ManageController(ProductDbContext db, IContextServices service)
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
        public StateOfOrder ChangeStateOfOrder(int orderId, StateOfOrder enumState)
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

            db.SaveChanges();
            return updateOrder.StateOfOrder;
        }


    }
}