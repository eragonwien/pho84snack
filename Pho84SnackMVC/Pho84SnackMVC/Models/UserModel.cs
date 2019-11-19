using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class UserModel
   {
      public long Id { get; set; }
      public string Email { get; set; }
      public string Name { get; set; }
      public string Surname { get; set; }
      public bool Active { get; set; }
   }
}
