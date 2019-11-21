using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Controllers
{
   public class AccountController : DefaultController
   {
      private readonly IUserRepository userRepository;
      private readonly ILogger<AccountController> log;

      public AccountController(IUserRepository userRepository, ILogger<AccountController> log)
      {
         this.userRepository = userRepository;
         this.log = log;
      }

      [AllowAnonymous]
      public IActionResult Login()
      {
         return View();
      }

      [AllowAnonymous]
      public IActionResult LoginExternal(string provider)
      {
         var authProperties = new AuthenticationProperties { RedirectUri = Url.Action(nameof(LoginCallback)) };
         return Challenge(authProperties, provider);
      }

      [AllowAnonymous]
      public async Task<IActionResult> LoginCallback()
      {
         var authResult = await HttpContext.AuthenticateAsync(Settings.SchemeExternal);
         if (!authResult.Succeeded)
         {
            return RedirectToAction(nameof(Login));
         }

         string email = authResult.Principal.FindFirstValue(ClaimTypes.Email);
         string lastname = authResult.Principal.FindFirstValue(ClaimTypes.Surname);
         string firstname = authResult.Principal.FindFirstValue(ClaimTypes.GivenName);
         string facebookAccessToken = authResult.Properties.GetTokenValue(AuthenticationSettings.TokenAccessToken);
         if (!await userRepository.Exists(email))
         {
            await userRepository.Create(new AppUser(email, lastname, firstname, facebookAccessToken));
         }
         AppUser appUser = await userRepository.GetOne(email);

         if (appUser.Active)
         {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, firstname));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, lastname));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeActive, appUser.Active.ToString()));
            claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessToken, facebookAccessToken));
            claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessTokenExpiredAt, authResult.Properties.GetTokenValue(AuthenticationSettings.TokenExpiredAt)));

            var authProperties = new AuthenticationProperties
            {
               AllowRefresh = true,
               ExpiresUtc = DateTimeOffset.Now.AddHours(1),
               IsPersistent = true
            };

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
         }
         
         await HttpContext.SignOutAsync(Settings.SchemeExternal);
         if (!appUser.Active)
         {
            Notify("Your account is registered and will be activated by Administator");
            RedirectToAction(nameof(Login));
         }
         return RedirectToAction("index", "home");
      }

      [AllowAnonymous]
      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync();
         return RedirectToAction("index", "home");
      }

      [Authorize]
      [HttpGet]
      public IActionResult NotActivated()
      {
         return View();
      }

      [Authorize]
      [HttpGet]
      public IActionResult AccessDenied()
      {
         if (!IsUserActive())
         {
            return RedirectToAction(nameof(NotActivated));
         }
         return View();
      }

      private bool IsUserActive()
      {
         bool isUserActive = bool.TryParse(User.FindFirstValue(AuthenticationSettings.ClaimTypeActive), out isUserActive) && isUserActive;
         return isUserActive;
      }
   }
}