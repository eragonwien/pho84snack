using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class PriceModalViewModel
   {
      public string ModalId { get; set; }
      public EditType EditType { get; set; }
      public long ProductId { get; set; }
      public List<SelectListItem> SizesSelect { get; set; }
      public long SizeId { get; set; }
      public decimal Price { get; set; }
      public long PriceId { get; set; }

      public PriceModalViewModel(string modalId, EditType editType, long productId, List<SelectListItem> sizes, long sizeId = -1, decimal price = -1, long priceId = -1)
      {
         ModalId = modalId;
         EditType = editType;
         ProductId = productId;
         SizesSelect = sizes ?? new List<SelectListItem>();
         SizeId = sizeId;
         Price = price;
         PriceId = priceId;
      }
   }
}
