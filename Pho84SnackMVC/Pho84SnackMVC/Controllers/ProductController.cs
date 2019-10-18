using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Controllers
{
   public class ProductController : DefaultController
   {
      private readonly IProductRepository productService;
      private readonly IErrorService errorService;
      private readonly ILogger<ProductController> log;

      public ProductController(IProductRepository productService, IErrorService errorService, ILogger<ProductController> log)
      {
         this.productService = productService;
         this.errorService = errorService;
         this.log = log;
      }

      // GET: Product
      public async Task<ActionResult> Index()
      {
         return View(await productService.GetAll());
      }

      // GET: Product/Details/5
      public async Task<ActionResult> Details(long id)
      {
         Product product = await productService.GetOne(id);
         ViewBag.Sizes = (await productService.Sizes()).Select(s => new SelectListItem(s.LongName, s.Id.ToString(), product.PriceList.Any(p => p.ProductSizeId == s.Id))).ToList();
         return View(await productService.GetOne(id));
      }

      // GET: Product/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: Product/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Create([FromForm] CreateViewModel model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               Product product = new Product(model);
               product.Id = await productService.Create(product);
               return RedirectToDetailPage(product.Id);
            }
            catch (MySqlException sqlex)
            {
               log.LogError("Fehler bei der Erstellung von Produkt {0}", model.Name, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei der Erstellung von Produkt {0}", model.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(model);
      }

      // GET: Product/Edit/5
      public async Task<ActionResult> Edit(long id)
      {
         return View(await productService.GetOne(id));
      }

      // POST: Product/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Edit([FromForm] Product product)
      {
         if (ModelState.IsValid)
         {
            try
            {
               await productService.Update(product);
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
      public async Task<ActionResult> Delete(long id)
      {
         try
         {
            await productService.Remove(id);
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Löschen vom Produkt {0}: {1}", id, ex.Message);
         }
         return RedirectToIndex();
      }
   }
}