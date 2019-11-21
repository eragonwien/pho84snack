using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using System.Diagnostics;

namespace Pho84SnackMVC.Controllers
{
   [AllowAnonymous]
   public class ErrorController : Controller
   {
      private readonly ILogger<ErrorController> log;

      public ErrorController(ILogger<ErrorController> log)
      {
         this.log = log;
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Index()
      {
         var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
         var ex = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
         log.LogError("[Unhandled Exception] RequestId={0}, MSG={1}, Stack={2}", requestId, ex.Error.Message, ex.Error.StackTrace);
         return View(new ErrorViewModel(requestId));
      }

   }
}