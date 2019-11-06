using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class Notification
   {
      public NotificationType Type { get; set; }
      public string Text { get; set; }

      public Notification(NotificationType type = NotificationType.undefined, string content = null)
      {
         Text = content;
         Type = type != NotificationType.undefined && !string.IsNullOrEmpty(content) ? type : NotificationType.undefined;
      }

      public static List<Notification> From(ModelStateDictionary modelState)
      {
         List<Notification> notifications = new List<Notification>();

         foreach (var item in modelState)
         {
            notifications.Add(new Notification(NotificationType.danger, string.Format("{0}: {1}", item.Key, item.Value)));
         }

         return notifications;
      }
   }

   public enum NotificationType
   {
      undefined,
      danger,
      warning,
      success,
      info
   }
}
