using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pho84SnackMVC.Services;

namespace Pho84SnackMVC.Controllers
{
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
      public ActionResult Index()
      {
         return View();
      }

      // GET: User/Details/5
      public ActionResult Details(int id)
      {
         return View();
      }

      // GET: User/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: User/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(IFormCollection collection)
      {
         try
         {
            // TODO: Add insert logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }

      // GET: User/Edit/5
      public ActionResult Edit(int id)
      {
         return View();
      }

      // POST: User/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(int id, IFormCollection collection)
      {
         try
         {
            // TODO: Add update logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }

      // GET: User/Delete/5
      public ActionResult Delete(int id)
      {
         return View();
      }

      // POST: User/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(int id, IFormCollection collection)
      {
         try
         {
            // TODO: Add delete logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }
   }
}