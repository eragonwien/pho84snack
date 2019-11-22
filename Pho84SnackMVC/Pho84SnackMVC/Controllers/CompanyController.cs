using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;
using System;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Controllers
{
   [Authorize(Policy = PolicySettings.Active)]
   public class CompanyController : DefaultController
   {
      private readonly ICompanyRepository companyRepository;
      private readonly ILogger<CompanyRepository> log;

      public CompanyController(ICompanyRepository companyRepository, ILogger<CompanyRepository> log)
      {
         this.companyRepository = companyRepository;
         this.log = log;
      }

      [HttpGet]
      public async Task<IActionResult> Edit()
      {
         if (!await companyRepository.Exists())
         {
            await companyRepository.Create(new Company());
         }
         return View(await companyRepository.Get());
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(long id, Company company)
      {
         if (id != company.Id)
         {
            log.LogError("Id vom Form(={0}) und vom URL(={1}) stimmen sich nicht überein", company.Id, id);
            ModelState.AddModelError("Id", "Id mismatch");
         }
         else if (!await companyRepository.Exists())
         {
            ModelState.AddModelError("", "Company does not exist");
         }
         else if (ModelState.IsValid)
         {
            try
            {
               await companyRepository.Update(company);
            }
            catch (Exception ex)
            {
               log.LogError("Fehler bei Erstellung von Kategorie {0}: {1}", company.Name, ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return View(company);
      }
   }
}