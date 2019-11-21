using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;
using System;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Controllers
{
   [Authorize(Policy = PolicySettings.Active)]
   public class UserController : DefaultController
   {
      private readonly IUserRepository userRepository;
      private readonly ILogger<UserController> log;

      public UserController(IUserRepository userRepository, ILogger<UserController> log)
      {
         this.userRepository = userRepository;
         this.log = log;
      }

      // GET: User
      public async Task<IActionResult> Index()
      {
         return View(await userRepository.GetAll());
      }

      // GET: User/Details/5
      public async Task<IActionResult> Details(long id)
      {
         if (!await userRepository.Exists(id))
         {
            return NotFound();
         }
         return View(await userRepository.GetOne(id));
      }

      // GET: User/Edit/5
      public async Task<IActionResult> Edit(long id)
      {
         if (!await userRepository.Exists(id))
         {
            return NotFound();
         }
         return View(await userRepository.GetOne(id));
      }

      // POST: User/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, AppUser appUser)
      {
         if (ModelState.IsValid)
         {
            try
            {
               await userRepository.Update(appUser);
               return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Bearbeitung von Benutzer {0}: {1}", appUser.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(appUser);
      }

      // POST: User/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(int id, string returnUrl)
      {
         try
         {
            ;
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Löschen von Benutzer {0}: {1}", id, ex.Message);
         }
         return LocalRedirect(returnUrl);
      }
   }
}