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
      public string Name { get; set; }
      public string Description { get; set; }
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

      public Category(CreateViewModel model)
      {
         Name = model.Name;
         Description = model.Description;
      }
   }
}
