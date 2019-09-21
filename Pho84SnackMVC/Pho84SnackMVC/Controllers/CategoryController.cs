using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;
using System;

namespace Pho84SnackMVC.Controllers
{
   public class CategoryController : Controller
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
         var model = new CategoryListViewModel(categoryService.GetAll());
         return View(model);
      }

      [HttpGet]
      public IActionResult Details(int id)
      {
         var category = categoryService.GetOne(id);
         return View(category);
      }

      [HttpGet]
      public IActionResult Edit(int id)
      {
         var category = categoryService.GetOne(id);
         return View(category);
      }

      [HttpPost]
      public IActionResult Edit([FromForm]Category category)
      {
         if (ModelState.IsValid)
         {
            try
            {
               categoryService.Update(category);
               return RedirectToAction(nameof(Details), new { @id = category.Id });
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Aktualisierung von Kategorie {0}-{1}: {2}", category.Id, category.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(category);
      }
   }
}