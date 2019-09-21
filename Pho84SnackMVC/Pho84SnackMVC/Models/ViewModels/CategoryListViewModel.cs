using System.Collections.Generic;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class CategoryListViewModel
   {
      public List<Category> Categories { get; set; }

      public CategoryListViewModel(List<Category> categories)
      {
         this.Categories = categories;
      }
   }
}
