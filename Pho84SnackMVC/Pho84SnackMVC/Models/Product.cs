using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pho84SnackMVC.Models.ViewModels;

namespace Pho84SnackMVC.Models
{
   public class Product
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public List<ProductPricing> PriceList { get; set; } = new List<ProductPricing>();

      public Product()
      {

      }

      public Product(string name, string description, int id = 0)
      {
         Id = id;
         Name = name;
         Description = description;
      }

      public Product(CreateViewModel model)
      {
         Name = model.Name;
         Description = model.Description;
      }
   }
}
