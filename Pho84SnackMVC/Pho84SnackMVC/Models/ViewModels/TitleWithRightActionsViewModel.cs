using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class TitleWithRightActionsViewModel
   {
      public string Title { get; set; }
      public string CreateUrl { get; set; }
      public string EditUrl { get; set; }

      public TitleWithRightActionsViewModel(string title, string createUrl = null, string editUrl = null)
      {
         Title = title;
         CreateUrl = createUrl;
         EditUrl = editUrl;
      }
   }
}
