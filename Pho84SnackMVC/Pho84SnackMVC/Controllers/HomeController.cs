﻿using System;
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
   public class HomeController : Controller
   {
      private readonly ICategoryService categoryService;
      private readonly ICompanyInfoService companyInfoService;
      private readonly IProductService productService;

      public HomeController(ICategoryService categoryService, ICompanyInfoService companyInfoService, IProductService productService, ILogger<HomeController> log)
      {
         this.categoryService = categoryService;
         this.companyInfoService = companyInfoService;
         this.productService = productService;
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
