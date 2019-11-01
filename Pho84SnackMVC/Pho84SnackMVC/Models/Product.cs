using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Pho84SnackMVC.Models.ViewModels;

namespace Pho84SnackMVC.Models
{
   public class Product
   {
      public long Id { get; set; }
      [MaxLength(32, ErrorMessage = "This name is too long (max. 32 characters)")]
      public string Name { get; set; }
      [MaxLength(256, ErrorMessage = "This name is too long (max. 256 characters)")]
      public string Description { get; set; }
      [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:#.00}", ApplyFormatInEditMode = true)]
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
