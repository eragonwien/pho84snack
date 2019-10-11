using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class ProductSizeViewModel
   {
      public long Id { get; set; } = 0;
      public long ProductId { get; set; }
      public long ProductSizeId { get; set; }
      public decimal Price { get; set; }

      public ProductSizeViewModel()
      {
      }

      public ProductSizeViewModel(long productId)
      {
         ProductId = productId;
      }

      public ProductSizeViewModel(long productId, long productSizeId, decimal price, long id = 0)
      {
         Id = id;
         ProductId = productId;
         ProductSizeId = productSizeId;
         Price = price;
      }
   }
}
