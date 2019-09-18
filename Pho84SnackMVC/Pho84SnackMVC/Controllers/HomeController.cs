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
   public class HomeController : Controller
   {
      private readonly ICategoryService categoryService;

      public HomeController(ICategoryService categoryService, ILogger<HomeController> log)
      {
         this.categoryService = categoryService;
      }

      public IActionResult Index()
      {
         var result = categoryService.GetAll();
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
