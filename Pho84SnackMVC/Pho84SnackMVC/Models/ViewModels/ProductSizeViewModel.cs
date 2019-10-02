using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class ProductSizeViewModel
   {
      public int Id { get; set; } = 0;
      public int ProductId { get; set; }
      public int ProductSizeId { get; set; }
      public decimal Price { get; set; }
      public List<SelectListItem> AvailableSizes { get; set; } = new List<SelectListItem>();

      public ProductSizeViewModel()
      {
      }

      public ProductSizeViewModel(int productId, List<ProductSize> productSizes)
      {
         ProductId = productId;
         AvailableSizes = productSizes.Select(s => new SelectListItem(string.Format("{0} - {1}", s.ShortName, s.LongName), s.Id.ToString())).ToList();
      }

      public ProductSizeViewModel(int productId, int productSizeId, decimal price, int id = 0)
      {
         Id = id;
         ProductId = productId;
         ProductSizeId = productSizeId;
         Price = price;
      }
   }
}
