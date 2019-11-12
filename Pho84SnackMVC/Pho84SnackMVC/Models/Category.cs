using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Pho84SnackMVC.Models.ViewModels;

namespace Pho84SnackMVC.Models
{
   public class Category
   {
      public long Id { get; set; }
      [Required]
      [MaxLength(32, ErrorMessage = "This name is too long (max. 32 characters)")]
      public string Name { get; set; }
      [MaxLength(256, ErrorMessage = "This name is too long (max. 256 characters)")]
      public string Description { get; set; }
      public List<long> SelectedProductIds { get; set; } = new List<long>();
      public List<Product> Products { get; set; } = new List<Product>();

      public Category()
      {

      }

      public Category(string name, string description, int id = 0)
      {
         Id = id;
         Name = name;
         Description = description;
      }
   }
}
