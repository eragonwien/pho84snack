using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class PriceModalViewModel
   {
      public string ModalId { get; set; }
      public long PriceId { get; set; }
      public EditType EditType { get; set; }
      public long ProductId { get; set; }
      public decimal Price { get; set; }
      public List<ProductSize> UnassignedSizes { get; set; } = new List<ProductSize>();

      public PriceModalViewModel(string modalId, EditType editType, long productId, List<ProductSize> unassignedSizes, long priceId = 0, decimal price = -1)
      {
         ModalId = modalId;
         EditType = editType;
         ProductId = productId;
         UnassignedSizes = unassignedSizes ?? UnassignedSizes;
         PriceId = priceId > 0 ? priceId : PriceId;
         Price = price;
      }
   }
}
