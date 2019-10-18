using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class RemoveModalViewModel
   {
      public string ModalId { get; set; }
      public string Url { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public string ReturnUrl { get; set; }

      public RemoveModalViewModel(string modalId, string url, string name, string type, string returnUrl = null)
      {
         ModalId = modalId;
         Url = url;
         Name = name;
         Type = type;
         ReturnUrl = returnUrl;
      }
   }
}
