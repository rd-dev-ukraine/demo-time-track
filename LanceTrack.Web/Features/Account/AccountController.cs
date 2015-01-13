using System;
using System.Web.Mvc;
using System.Web.Security;
using FluentValidation;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Web.Features.Account.Models;

namespace LanceTrack.Web.Features.Account
{
    public partial class AccountController : Controller
    {
        private readonly IUserAccountService _userAccountService;

        public AccountController(IUserAccountService userAccountService)
        {
            if (userAccountService == null)
                throw new ArgumentNullException("userAccountService");

            _userAccountService = userAccountService;
        }

        [HttpGet]
        public virtual ActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Login(LoginModel formModel)
        {
            try
            {
                new LoginModelValidator().ValidateAndThrow(formModel);
                var user = _userAccountService.Login(formModel.Login, formModel.Password);

                FormsAuthentication.SetAuthCookie(user.Email, formModel.RememberMe);

                return RedirectToAction(MVC.Home.Index());
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            catch (LoginFailedException ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(formModel);
        }

        [HttpGet]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Home.Index());
        }

    }
}