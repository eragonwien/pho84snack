using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class PriceViewModel
   {
      public int Id { get; set; }
      public long ProductId { get; set; }
      public int SizeId { get; set; }
      public decimal Price { get; set; }
   }
}
