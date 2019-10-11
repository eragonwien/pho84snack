using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;
using System;
using System.Collections.Generic;

namespace Pho84SnackMVC.Controllers
{
   public class CategoryController : DefaultController
   {
      private readonly ICategoryService categoryService;
      private readonly IProductService productService;
      private readonly IErrorService errorService;
      private readonly ILogger<CategoryController> log;

      public CategoryController(ICategoryService categoryService, IProductService productService, IErrorService errorService, ILogger<CategoryController> log)
      {
         this.categoryService = categoryService;
         this.productService = productService;
         this.errorService = errorService;
         this.log = log;
      }

      [HttpGet]
      public IActionResult Index()
      {
         var model = categoryService.GetAll();
         return View(model);
      }

      [HttpGet]
      public IActionResult Details(long id)
      {
         var category = categoryService.GetOne(id);
         ViewBag.AssignableProducts = productService.GetAll();
         return View(category);
      }

      [HttpGet]
      public ActionResult Create()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([FromForm]CategoryCreateViewModel model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               Category category = new Category(model);
               category.Id = categoryService.Create(category);
               return RedirectToDetailPage(category.Id);
            }
            catch (MySqlException sqlex)
            {
               log.LogError("SQL Fehler bei Erstellung von Kategorie {0}: {1}", model.Name, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Erstellung von Kategorie {0}: {1}", model.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(long id)
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
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Aktualisierung von Kategorie {0}, Prop={1}: {2}", category.Id, propertyName, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return BadRequest(ModelState);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Assign(long categoryId, IEnumerable<long> productIds)
      {
         bool hasError = false;

         // Zuweisung von Produkten
         foreach (long productId in productIds)
         {
            try
            {
               categoryService.Assign(categoryId, productId);
            }
            catch (MySqlException sqlex)
            {
               log.LogError("Fehler bei Zuweisung von Produkt {0} auf Kategorie {1}: {2}", productId, categoryId, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
               hasError = true;
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Zuweisung von Produkt {0} auf Kategorie {1}: {2}", productId, categoryId, ex.Message);
               ModelState.AddModelError("", ex.Message);
               hasError = true;
            }
         }

         if (!hasError)
         {
            // Entfernung von nicht-zugewiesenden Produkten
            try
            {
               categoryService.RemoveAssigned(categoryId, productIds);
            }
            catch (MySqlException sqlex)
            {
               log.LogError("Fehler bei Entfernung von Produkten aus der Kategorie {0}: {1}", categoryId, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
               hasError = true;
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Entfernung von Produkten aus der Kategorie {0}: {1}", categoryId, ex.Message);
               ModelState.AddModelError("", ex.Message);
               hasError = true;
            }
         }

         if (!hasError)
         {
            return Ok();
         }
         return BadRequest(ModelState);
      }
   }
}