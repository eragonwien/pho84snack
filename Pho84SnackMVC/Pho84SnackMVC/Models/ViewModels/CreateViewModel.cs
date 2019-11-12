using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class CreateViewModel
   {
      public string ControllerName { get; set; }
      public string Name { get; set; }
      public string Value { get; set; }
      public string Placeholder { get; set; }

      public CreateViewModel(string controllerName, string name, string value = null, string placeholder = null)
      {
         ControllerName = controllerName;
         Name = name;
         Value = value;
         Placeholder = placeholder;
      }
   }
}
