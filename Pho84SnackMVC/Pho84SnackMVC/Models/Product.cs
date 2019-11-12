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
      public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

      public Product()
      {

      }

      public Product(long id = 0, string name = null, string description = null, List<ProductSize> productSizes = null)
      {
         Id = id;
         Name = name;
         Description = description;
         ProductSizes = productSizes ?? new List<ProductSize>();
      }
   }
}
