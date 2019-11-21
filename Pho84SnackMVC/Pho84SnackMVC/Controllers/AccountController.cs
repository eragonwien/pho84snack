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
   [AllowAnonymous]
   public class AccountController : DefaultController
   {
      private readonly IUserRepository userRepository;
      private readonly ILogger<AccountController> log;

      public AccountController(IUserRepository userRepository, ILogger<AccountController> log)
      {
         this.userRepository = userRepository;
         this.log = log;
      }

      public IActionResult Login()
      {
         return View();
      }

      public IActionResult LoginExternal(string provider)
      {
         var authProperties = new AuthenticationProperties { RedirectUri = Url.Action(nameof(LoginCallback)) };
         return Challenge(authProperties, provider);
      }

      public async Task<IActionResult> LoginCallback()
      {
         var authResult = await HttpContext.AuthenticateAsync(Settings.SchemeExternal);
         if (!authResult.Succeeded)
         {
            return RedirectToAction(nameof(Login));
         }

         string email = authResult.Principal.FindFirstValue(ClaimTypes.Email);
         string name = authResult.Principal.FindFirstValue(ClaimTypes.Name);
         string surname = authResult.Principal.FindFirstValue(ClaimTypes.Surname);
         if (! await userRepository.Exists(email))
         {
            await userRepository.Create(new AppUser(email, name, surname));
         }
         AppUser appUser = await userRepository.GetOne(email);

         var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, email));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, surname));
         claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, appUser.Role.Name));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessToken, authResult.Properties.GetTokenValue(AuthenticationSettings.TokenAccessToken)));
         claimsIdentity.AddClaim(new Claim(AuthenticationSettings.ClaimTypeAccessTokenExpiredAt, authResult.Properties.GetTokenValue(AuthenticationSettings.TokenExpiredAt)));

         var authProperties = new AuthenticationProperties
         {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(30),
            IsPersistent = true
         };

         await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
         await HttpContext.SignOutAsync(Settings.SchemeExternal);

         return RedirectToAction("index", "home");
      }

      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync();
         return RedirectToAction("index", "home");
      }
   }
}