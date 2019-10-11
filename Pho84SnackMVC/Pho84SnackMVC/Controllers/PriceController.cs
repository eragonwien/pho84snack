using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;

namespace Pho84SnackMVC.Controllers
{
    public class PriceController : DefaultController
    {
      private readonly IPriceService priceService;
      private readonly IErrorService errorService;
      private readonly ILogger<PriceController> log;

      public PriceController(IPriceService productService, IErrorService errorService, ILogger<PriceController> log)
      {
         this.priceService = productService;
         this.errorService = errorService;
         this.log = log;
      }

      // POST: Price/Add
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Add([FromForm] PriceViewModel model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               long id = await priceService.Add(model);
               return Ok();
            }
            catch (MySqlException sqlex)
            {
               log.LogError("SQL Fehler bei Erstellung vom Preis für die Größe {0} von Produkt {1}: {2}", model.SizeId, model.ProductId, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
            }
            catch (Exception ex)
            {
               log.LogError("SQL Fehler bei Erstellung vom Preis für die Größe {0} von Produkt {1}: {2}", model.SizeId, model.ProductId, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return BadRequest(ModelState);
      }

      // POST: Price/Price
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Edit(long id, [FromForm] PriceViewModel model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               await priceService.Price(id, model);
               return Ok();
            }
            catch (MySqlException sqlex)
            {
               log.LogError("SQL Fehler bei der Löschung vom Preis {0}: {1}", id, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
            }
            catch (Exception ex)
            {
               log.LogError("SQL Fehler bei der Löschung vom Preis {0}: {1}", id, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return BadRequest(ModelState);
      }

      // POST: Price/Remove
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Remove(long id)
      {
         if (ModelState.IsValid)
         {
            try
            {
               await priceService.Remove(id);
            }
            catch (MySqlException sqlex)
            {
               log.LogError("SQL Fehler bei der Löschung vom Preis {0}: {1}", id, sqlex.Message);
               var modelerror = errorService.HandleException(sqlex);
               ModelState.AddModelError(modelerror.Item1, modelerror.Item2);
            }
            catch (Exception ex)
            {
               log.LogError("SQL Fehler bei der Löschung vom Preis {0}: {1}", id, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return RedirectToIndexPermanent();
      }
   }
}