using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pho84SnackMVC.Models.ViewModels;

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

      public RedirectToActionResult RedirectToIndexPermanent()
      {
         return RedirectToActionPermanent("Index");
      }

      internal string GetPropertyValue(object src, string propName)
      {
         return src.GetType().GetProperty(propName).GetValue(src, null)?.ToString();
      }

      internal void SetViewBagReturnUrl(string returnurl)
      {
         ViewBag.ReturnUrl = Url.IsLocalUrl(returnurl) ? returnurl : Url.Action("index", "home");
      }

      public IActionResult RedirectToReturnUrl(string returnUrl)
      {
         if (Url.IsLocalUrl(returnUrl))
         {
            return Redirect(returnUrl);
         }
         return Redirect(Url.Action("index", "home"));
      }

      public IActionResult RedirectPermanentToReturnUrl(string returnUrl)
      {
         if (Url.IsLocalUrl(returnUrl))
         {
            return RedirectPermanent(returnUrl);
         }
         return RedirectToActionPermanent(Url.Action("index", "home"));
      }

      public void NotifyModelStateError()
      {
         var notifications = TempData["Notifications"] != null ? TempData["Notifications"] as List<string> : new List<string>();
         if (!ModelState.IsValid)
         {
            notifications = ModelState.SelectMany(v => v.Value.Errors.Select(e => string.Format("{0}: {1}", v.Key, e.ErrorMessage))).ToList();
         }
         TempData["Notifications"] = notifications;
      }

      public void Notify(string message)
      {
         var notifications = TempData["Notifications"] != null ? TempData["Notifications"] as List<string> : new List<string>();
         notifications.Add(message);
         TempData["Notifications"] = notifications;
      }
   }
}