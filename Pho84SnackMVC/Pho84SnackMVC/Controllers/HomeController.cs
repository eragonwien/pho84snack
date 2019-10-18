using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;

namespace Pho84SnackMVC.Controllers
{
   public class HomeController : DefaultController
   {
      private readonly ICategoryRepository categoryService;
      private readonly ICompanyInfoRepository companyInfoService;
      private readonly IProductRepository productService;
      private readonly ILogger<HomeController> log;

      public HomeController(ICategoryRepository categoryService, ICompanyInfoRepository companyInfoService, IProductRepository productService, ILogger<HomeController> log)
      {
         this.categoryService = categoryService;
         this.companyInfoService = companyInfoService;
         this.productService = productService;
         this.log = log;
      }

      public IActionResult Index()
      {
         return View();
      }

      public IActionResult Privacy()
      {
         return View();
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}
