using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pho84SnackMVC.Controllers
{
   public class DefaultController : Controller
   {
      public RedirectToActionResult RedirectToDetailPage(long id)
      {
         return RedirectToAction("Details", new { id });
      }

      public RedirectToActionResult RedirectToIndex()
      {
         return RedirectToAction("Index");
      }

      public RedirectToActionResult RedirectToIndexPermanent(bool permanent = false)
      {
         return RedirectToActionPermanent("Index");
      }
   }
}