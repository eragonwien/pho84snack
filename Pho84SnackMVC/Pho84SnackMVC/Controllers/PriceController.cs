using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using Pho84SnackMVC.Services;

namespace Pho84SnackMVC.Controllers
{
   [Authorize]
   public class PriceController : DefaultController
    {
      private readonly IPriceRepository priceRepository;
      private readonly ILogger<PriceController> log;

      public PriceController(IPriceRepository IProductRepository, ILogger<PriceController> log)
      {
         this.priceRepository = IProductRepository;
         this.log = log;
      }

      // POST: Price/Add
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Add([FromForm] ProductSize model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               long id = await priceRepository.Add(model);
               return Ok();
            }
            catch (Exception ex)
            {
               log.LogError("SQL Fehler bei Erstellung vom Preis für die Größe {0} von Produkt {1}: {2}", model.Size.Id, model.Product.Id, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return BadRequest(ModelState);
      }

      // POST: Price/Price
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Edit([FromForm] ProductSize model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               await priceRepository.Price(model);
               return Ok();
            }
            catch (Exception ex)
            {
               log.LogError("SQL Fehler bei der Löschung vom Preis {0}: {1}", model.Id, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return BadRequest(ModelState);
      }

      // POST: Price/Remove
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Remove(long id, string returnUrl)
      {
         if (ModelState.IsValid)
         {
            try
            {
               await priceRepository.Remove(id);
            }
            catch (Exception ex)
            {
               log.LogError("SQL Fehler bei der Löschung vom Preis {0}: {1}", id, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return RedirectPermanentToReturnUrl(returnUrl);
      }
   }
}