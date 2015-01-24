using System;
using System.Web.Http;
using System.Web.Security;
using FluentValidation;
using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Web.Features.Authorization
{
    [RoutePrefix("api/authorization")]
    public class AuthorizationApiController : ApiController
    {
        private readonly IUserAccountService _userAccountService;

        public AuthorizationApiController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [Route("login", Name = "Login"), HttpPost]
        public virtual IHttpActionResult Login(LoginModel formModel)
        {
            try
            {
                new LoginModelValidator().ValidateAndThrow(formModel);
                var user = _userAccountService.Login(formModel.Login, formModel.Password);

                FormsAuthentication.SetAuthCookie(user.Email, formModel.RememberMe);

                return Ok();
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);}
            catch (LoginFailedException ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return BadRequest(ModelState);
        }

        [Route("logout", Name="Logout"), HttpPost, Authorize]
        public virtual void Logout()
        {
            FormsAuthentication.SignOut();
        }

        [Route("current-user", Name="CurrentUser"), HttpGet, Authorize]
        public virtual UserAccount CurrentUser()
        {
            return _userAccountService.FindByEmail(RequestContext.Principal.Identity.Name);
        }


    }
}