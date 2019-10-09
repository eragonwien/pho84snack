using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;
using System;
using System.Linq;

namespace Pho84SnackMVC.Controllers
{
   public class CategoryController : DefaultController
   {
      private readonly ICategoryService categoryService;
      private readonly IProductService productService;
      private readonly ILogger<CategoryController> log;

      public CategoryController(ICategoryService categoryService, IProductService productService, ILogger<CategoryController> log)
      {
         this.categoryService = categoryService;
         this.productService = productService;
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

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Patch([FromForm]Category category, [FromForm]string propertyName)
      {
         if (ModelState.IsValid)
         {
            try
            {
               string patchValue = GetPropertyValue(category, propertyName);
               categoryService.Patch(category.Id, propertyName, patchValue);
               return Ok(patchValue);
            }
            catch (MySqlException sqlex)
            {
               log.LogError("Fehler bei Aktualisierung von Kategorie {0}, Prop={1}: {2}", category.Id, propertyName, sqlex.Message);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Aktualisierung von Kategorie {0}, Prop={1}: {2}", category.Id, propertyName, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return BadRequest(ModelState);
      }
   }
}