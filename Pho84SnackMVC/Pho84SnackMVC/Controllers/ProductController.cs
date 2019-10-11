using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;
using System;

namespace Pho84SnackMVC.Controllers
{
   public class ProductController : DefaultController
   {
      private readonly IProductService productService;
      private readonly IErrorService errorService;
      private readonly ILogger<ProductController> log;

      public ProductController(IProductService productService, IErrorService errorService, ILogger<ProductController> log)
      {
         this.productService = productService;
         this.errorService = errorService;
         this.log = log;
      }

      // GET: Product
      public ActionResult Index()
      {
         return View(productService.GetAll());
      }

      // GET: Product/Details/5
      public ActionResult Details(long id)
      {
         return View(productService.GetOne(id));
      }

      // GET: Product/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: Product/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([FromForm] Product product)
      {
         if (ModelState.IsValid)
         {
            try
            {
               long id = productService.Create(product);
               return RedirectToDetailPage(id);
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
         return View(product);
      }

      // GET: Product/Edit/5
      public ActionResult Edit(long id)
      {
         return View(productService.GetOne(id));
      }

      // POST: Product/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([FromForm] Product product)
      {
         if (ModelState.IsValid)
         {
            try
            {
               productService.Update(product);
               return RedirectToDetailPage(product.Id);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Bearbeitung von Produkt: {0}", ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(product);
      }

      // POST: Product/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(long id)
      {
         try
         {
            productService.Remove(id);
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Löschen vom Produkt {0}: {1}", id, ex.Message);
         }
         return RedirectToIndex();
      }

      #region Price

      // GET: Product/CreatePrice
      [HttpGet]
      public ActionResult CreatePrice(long productId)
      {
         var model = new ProductSizeViewModel(productId, productService.GetProductSizes(productId));
         return View(model);
      }

      // POST: Product/CreatePrice
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult CreatePrice([FromForm] ProductSizeViewModel model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               long id = productService.AddPrice(model);
               return RedirectToDetailPage(model.ProductId);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Hinzufügen vom Price in Produkt {0}: {1}", model.ProductId, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(model);
      }

      #endregion
   }
}