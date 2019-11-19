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
   [Authorize]
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
      public IActionResult LoginExternal()
      {
         var authProperties = new AuthenticationProperties { RedirectUri = Url.Action(nameof(LoginCallback)) };
         return Challenge(authProperties, FacebookDefaults.AuthenticationScheme);
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
         if (!userRepository.Exists(email))
         {
            userRepository.Create(email);
         }
         UserModel userModel = userRepository.GetOne(email);
         
         var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, authResult.Principal.FindFirstValue(ClaimTypes.Email)));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, authResult.Principal.FindFirstValue(ClaimTypes.Name)));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, authResult.Principal.FindFirstValue(ClaimTypes.Surname)));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeActive, userModel.Active.ToString()));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessToken, authResult.Properties.GetTokenValue(AuthenticationSettings.TokenAccessToken)));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessTokenExpiredAt, authResult.Properties.GetTokenValue(AuthenticationSettings.TokenExpiredAt)));

         var authProperties = new AuthenticationProperties
         {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(30),
            IsPersistent = true
         };

         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
         await HttpContext.SignOutAsync(Settings.SchemeExternal);

         return RedirectToAction("index", userModel.Active ? "home" : "privacy");
      }

      [AllowAnonymous]
      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync();
         return RedirectToAction("index", "home");
      }
   }
}