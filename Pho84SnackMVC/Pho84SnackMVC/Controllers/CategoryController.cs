using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pho84SnackMVC.Models.ViewModels;
using System.Collections;
using System.Collections.Generic;

namespace Pho84SnackMVC.Controllers
{
   public class CategoryController : DefaultController
   {
      private readonly IErrorService errorService;
      private readonly ICategoryRepository categoryRepository;
      private readonly ILogger<CategoryController> log;

      public CategoryController(ICategoryRepository categoryRepository, IErrorService errorService, ILogger<CategoryController> log)
      {
         this.categoryRepository = categoryRepository;
         this.errorService = errorService;
         this.log = log;
      }

      [HttpGet]
      public async Task<IActionResult> Index(List<Notification> notifications)
      {
         var model = await categoryRepository.GetAll();
         ViewBag.Notifications = notifications;
         return View(model);
      }

      [HttpGet]
      public async Task<IActionResult> Details(long id)
      {
         if (!await categoryRepository.Exists(id))
         {
            return NotFound(id);
         }
         var category = await categoryRepository.GetOne(id);
         ViewBag.Edit = false;
         return View(category);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Details(long id, [FromForm]Category category)
      {
         if (id != category.Id)
         {
            log.LogError("Id vom Form(={0}) und vom URL(={1}) stimmen sich nicht überein", category.Id, id);
            return BadRequest(id);
         }
         if (!await categoryRepository.Exists(id))
         {
            return NotFound(id);
         }
         if (ModelState.IsValid)
         {
            try
            {
               await categoryRepository.Update(category);
               return RedirectToDetailPage(id);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Erstellung von Kategorie {0}: {1}", category.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         ViewBag.Edit = true;
         return View(category);
      }

      [HttpGet]
      public ActionResult Create()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([FromForm]Category category)
      {
         if (ModelState.IsValid)
         {
            try
            {
               if (!await categoryRepository.Exists(category.Name))
               {
                  category.Id = await categoryRepository.Create(category);
                  return RedirectToIndex();
               }
               else
               {
                  log.LogError("Kategorie mit dem Name {0} existiert bereits", category.Name);
                  ModelState.AddModelError(nameof(category.Name), string.Format("This name is already taken"));
               }
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Erstellung von Kategorie {0}: {1}", category.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return RedirectToIndex();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Delete(long id)
      {
         try
         {
            await categoryRepository.Delete(id);
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Löschen von Kategorie {0}: {1}", id, ex.Message);
         }
         return RedirectToIndex();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Unassign(long id, long productId, string returnUrl)
      {
         try
         {
            await categoryRepository.Unassign(id, productId);
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Entfernung von Produkt {0} aus der Kategorie {1}: {1}", productId, id, ex.Message);
         }
         return RedirectPermanentToReturnUrl(returnUrl);
      }
   }
}