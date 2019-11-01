
using System.ComponentModel.DataAnnotations;

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

      public ProductPricing()
      {

      }

      public ProductPricing(int id, int productId, int productSizeId, string shortName, string longName, decimal price)
      {
         Id = id;
         ProductId = productId;
         ProductSizeId = productSizeId;
         ShortName = shortName;
         LongName = longName;
         Price = price;
      }

      public string DisplayPrice
      {
         get
         {
            return Price > 0 ? string.Format("€{0:N2}", Price) : string.Empty;
         }
      }
   }
}
