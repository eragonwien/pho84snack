using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pho84SnackMVC.Models;

namespace Pho84SnackMVC.Services
{
   public interface IUserRepository
   {
      bool Exists(string email);
      void Create(string email);
      UserModel GetOne(string email);
   }

   public class UserRepository : IUserRepository
   {
      public void Create(string email)
      {
         throw new NotImplementedException();
      }

      public bool Exists(string email)
      {
         throw new NotImplementedException();
      }

      public UserModel GetOne(string email)
      {
         throw new NotImplementedException();
      }
   }
}
