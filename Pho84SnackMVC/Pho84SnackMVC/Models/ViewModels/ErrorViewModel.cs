using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class ErrorViewModel
   {
      public string Id { get; set; }

      public ErrorViewModel(string requestId)
      {
         this.Id = requestId;
      }
   }
}
