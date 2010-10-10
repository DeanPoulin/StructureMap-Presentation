using System;
using System.Web.Mvc;
using System.Web.Security;
using DeanIS.YBSquare.Web.Services;
using DeanIS.YBSquare.WebSite.Models;
using DeanIS.YBSquare.WebSite.Models.Validation;

namespace DeanIS.YBSquare.WebSite.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Invoked via constructor injection.
        /// </summary>
        /// <param name="formsService"></param>
        /// <param name="membershipService"></param>
        public AccountController(IFormsAuthenticationService formsService, 
                                    IMembershipService membershipService)
        {
            _formsService = formsService;
            _membershipService = membershipService;
        }

        // services used in this controller.
        private readonly IFormsAuthenticationService _formsService;
        private readonly IMembershipService _membershipService;

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_membershipService.ValidateUser(model.Email, model.Password))
                {
                    _formsService.SignIn(model.Email, model.RememberMe);
                    return !String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                               ? (ActionResult) Redirect(returnUrl)
                               : RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            _formsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewModel.PasswordLength = _membershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = _membershipService.CreateUser(model.Email, model.Password);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    _formsService.SignIn(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
            }

            // If we got this far, something failed, redisplay form
            ViewModel.PasswordLength = _membershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewModel.PasswordLength = _membershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (_membershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            ViewModel.PasswordLength = _membershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
