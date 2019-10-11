using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class ProductPricing
   {
      public long Id { get; set; }
      public long ProductId { get; set; }
      public long ProductSizeId { get; set; }
      public string ShortName { get; set; }
      public string LongName { get; set; }
      public decimal Price { get; set; }

      public ProductPricing(int id, int productId, int productSizeId, string shortName, string longName, decimal price)
      {
         Id = id;
         ProductId = productId;
         ProductSizeId = productSizeId;
         ShortName = shortName;
         LongName = longName;
         Price = price;
      }
   }
}
