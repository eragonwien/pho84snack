using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class RemoveModalViewModel
   {
      public string ModalId { get; set; }
      public string Url { get; set; }
      public long Id { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }

      public RemoveModalViewModel(string modalId, string url, long id, string name, string type)
      {
         ModalId = modalId;
         Url = url;
         Id = id;
         Name = name;
         Type = type;
      }
   }
}
