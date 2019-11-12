using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Controllers
{
   public class ProductController : DefaultController
   {
      private readonly IProductRepository productRepository;
      private readonly IPriceRepository priceRepository;
      private readonly IErrorService errorService;
      private readonly ILogger<ProductController> log;

      public ProductController(IProductRepository productRepository, IPriceRepository priceRepository, IErrorService errorService, ILogger<ProductController> log)
      {
         this.productRepository = productRepository;
         this.priceRepository = priceRepository;
         this.errorService = errorService;
         this.log = log;
      }

      // GET: Product
      public async Task<ActionResult> Index()
      {
         return View(await productRepository.GetAll());
      }

      // GET: Product/Details/5
      public async Task<ActionResult> Details(long id)
      {
         if (!await productRepository.Exists(id))
         {
            return NotFound(id);
         }
         Product product = await productRepository.GetOne(id);
         return View(product);
      }

      // GET: Product/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: Product/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Create([FromForm] Product product)
      {
         if (ModelState.IsValid)
         {
            try
            {
               if (await productRepository.Exists(product.Name))
               {
                  ModelState.AddModelError(nameof(product.Name), "This Name already taken");
               }
               product.Id = await productRepository.Create(product);
               return RedirectToDetailPage(product.Id);
            }
            catch (MySqlException sqlex)
            {
               log.LogError("Fehler bei der Erstellung von Produkt {0}", product.Name, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei der Erstellung von Produkt {0}", product.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(product);
      }

      // GET: Product/Edit/5
      public async Task<ActionResult> Edit(long id)
      {
         return View(await productRepository.GetOne(id));
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
               await productRepository.Update(product);
               await priceRepository.Update(product.Id, product.ProductSizes);
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
            await productRepository.Delete(id);
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Löschen vom Produkt {0}: {1}", id, ex.Message);
         }
         return RedirectToIndex();
      }
   }
}