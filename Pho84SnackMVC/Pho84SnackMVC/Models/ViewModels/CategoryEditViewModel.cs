using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class CategoryEditViewModel
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public List<string> ProductIds { get; set; } = new List<string>();
      public List<SelectListItem> AvailableProducts { get; set; } = new List<SelectListItem>();

      public CategoryEditViewModel()
      {

      }

      public CategoryEditViewModel(Category category, List<Product> availableProducts)
      {
         Id = category.Id;
         Name = category.Name;
         Description = category.Description;
         ProductIds = category.Products.Select(p => p.Id.ToString()).ToList();
         AvailableProducts = availableProducts.Select(p => new SelectListItem(p.Name, p.Id.ToString(), category.Products.Any(pr => pr.Id == p.Id))).ToList();
      }
   }
}
