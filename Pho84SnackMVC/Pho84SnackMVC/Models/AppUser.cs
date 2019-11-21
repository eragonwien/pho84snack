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
      public string Firstname { get; set; }
      public string FacebookAccessToken { get; set; }
      public bool Active { get; set; } = false;

      public AppUser()
      {

      }

      public AppUser(long id, string email, string lastname, string firstname, string facebookAccessToken, bool active)
      {
         Id = id;
         Email = email;
         Lastname = lastname;
         Firstname = firstname;
         FacebookAccessToken = facebookAccessToken;
         Active = active;
      }

      public AppUser(string email, string lastname = null, string firstname = null, string facebookAccessToken = null)
      {
         Email = email;
         Lastname = lastname;
         Firstname = firstname;
         FacebookAccessToken = facebookAccessToken;
      }

      public string Name
      {
         get { return Firstname + " " + Lastname; }
      }
   }
}
