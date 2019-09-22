using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;
using System;

namespace Pho84SnackMVC.Controllers
{
   public class CategoryController : DefaultController
   {
      private readonly ICategoryService categoryService;
      private readonly ILogger<CategoryController> log;

      public CategoryController(ICategoryService categoryService, ILogger<CategoryController> log)
      {
         this.categoryService = categoryService;
         this.log = log;
      }

      [HttpGet]
      public IActionResult Index()
      {
         var model = categoryService.GetAll();
         return View(model);
      }

      [HttpGet]
      public IActionResult Details(int id)
      {
         var category = categoryService.GetOne(id);
         return View(category);
      }

      [HttpGet]
      public ActionResult Create()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([FromForm]Category category)
      {
         if (ModelState.IsValid)
         {
            try
            {
               long id = categoryService.Create(category);
               return RedirectToDetailPage(id);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Erstellung von Kategorie: {0}", ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(category);
      }

      [HttpGet]
      public IActionResult Edit(int id)
      {
         var category = categoryService.GetOne(id);
         return View(category);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Edit([FromForm]Category category)
      {
         if (ModelState.IsValid)
         {
            try
            {
               categoryService.Update(category);
               return RedirectToDetailPage(category.Id);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Aktualisierung von Kategorie {0}-{1}: {2}", category.Id, category.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(category);
      }

      // POST: Category/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(int id)
      {
         try
         {
            categoryService.Remove(id);
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Löschen von Kategorie {0}: {1}", id, ex.Message);
         }
         return RedirectToIndex();
      }
   }
}