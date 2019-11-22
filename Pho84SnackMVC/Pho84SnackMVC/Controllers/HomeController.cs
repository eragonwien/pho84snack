﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;

namespace Pho84SnackMVC.Controllers
{
   [Authorize]
   public class HomeController : DefaultController
   {
      private readonly ICategoryRepository categoryRepository;
      private readonly ICompanyRepository companyInfoRepository;
      private readonly IProductRepository productRepository;
      private readonly ILogger<HomeController> log;

      public HomeController(ICategoryRepository categoryRepository, ICompanyRepository companyInfoRepository, IProductRepository productRepository, ILogger<HomeController> log)
      {
         this.categoryRepository = categoryRepository;
         this.companyInfoRepository = companyInfoRepository;
         this.productRepository = productRepository;
         this.log = log;
      }

      public IActionResult Index()
      {
         return RedirectToAction("index", "category");
      }

      public IActionResult Privacy()
      {
         return View();
      }
   }
}
