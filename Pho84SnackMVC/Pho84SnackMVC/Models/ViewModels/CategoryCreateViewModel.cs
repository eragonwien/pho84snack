using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class CategoryCreateViewModel
   {
      [Required]
      public string Name { get; set; }
      public string Description { get; set; }
   }
}
