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
      private readonly ICategoryRepository categoryRepository;
      private readonly ICompanyInfoRepository companyInfoRepository;
      private readonly IProductRepository productRepository;
      private readonly ILogger<HomeController> log;

      public HomeController(ICategoryRepository categoryRepository, ICompanyInfoRepository companyInfoRepository, IProductRepository productRepository, ILogger<HomeController> log)
      {
         this.categoryRepository = categoryRepository;
         this.companyInfoRepository = companyInfoRepository;
         this.productRepository = productRepository;
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
   }
}
