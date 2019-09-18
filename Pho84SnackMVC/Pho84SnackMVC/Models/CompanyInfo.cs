using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class CompanyInfo
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public string Address { get; set; }
      public string AddressExtra { get; set; }
      public string Zip { get; set; }
      public string City { get; set; }
      public string Phone { get; set; }
      public string Email { get; set; }
      public string Facebook { get; set; }
   }
}
