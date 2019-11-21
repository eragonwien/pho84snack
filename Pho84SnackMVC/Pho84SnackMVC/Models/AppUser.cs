using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class AppUser
   {
      public long Id { get; set; } = 0;
      public string Email { get; set; }
      public string Lastname { get; set; }
      public string Surname { get; set; }
      public string FacebookAccessToken { get; set; }
      public Role Role { get; set; }

      public AppUser()
      {

      }

      public AppUser(long id, string email, string lastname, string surname, string facebookAccessToken, Role role)
      {
         Id = id;
         Email = email;
         Lastname = lastname;
         Surname = surname;
         FacebookAccessToken = facebookAccessToken;
         Role = role;
      }

      public AppUser(string email, string lastname = null, string surname = null, string facebookAccessToken = null)
      {
         Email = email;
         Lastname = lastname;
         Surname = surname;
         FacebookAccessToken = facebookAccessToken;
      }
   }
}
