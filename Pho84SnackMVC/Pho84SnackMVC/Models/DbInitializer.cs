using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class DbInitializer
   {
      public static void Initialize(ICategoryService categoryService)
      {
         if (categoryService.Count() == 0)
         {
            categoryService.Create(new Category("Test category", "A test category"));
         }
      }
   }
}
