using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;

namespace Pho84SnackMVC.Services
{
   public interface IUserRepository
   {
      Task<bool> Exists(string email);
      Task Create(AppUser user);
      Task<AppUser> GetOne(string email);
      Task<List<AppUser>> GetAll();
   }

   public class UserRepository : IUserRepository
   {
      private readonly Pho84SnackContext context;

      public UserRepository(Pho84SnackContext context)
      {
         this.context = context;
      }

      public async Task Create(AppUser user)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = @"insert into APPUSER(Email, Lastname, Surname, FacebookAccessToken, RoleId) values(@Email, @Lastname, @Surname, @FacebookAccessToken, (select Id from Role where Name=@RoleName))";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Email", user.Email));
               cmd.Parameters.Add(new MySqlParameter("@Lastname", user.Lastname));
               cmd.Parameters.Add(new MySqlParameter("@Surname", user.Surname));
               cmd.Parameters.Add(new MySqlParameter("@FacebookAccessToken", user.FacebookAccessToken));
               cmd.Parameters.Add(new MySqlParameter("@RoleName", Role.RoleBasic));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public async Task<bool> Exists(string email)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = @"select count(*) from APPUSER where Email=@Email";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Email", email));
               await con.OpenAsync();
               return await cmd.ReadScalarInt32() > 0;
            }
         }
      }

      public async Task<List<AppUser>> GetAll()
      {
         List<AppUser> users = new List<AppUser>();
         using (var con = context.GetConnection())
         {
            string cmdStr = @"select Id, Email, Lastname, Surname, FacebookAccessToken, RoleId, RoleName, RoleDescription from V_USER";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     Role userRole = new Role(odr.ReadInt("RoleId"), odr.ReadString("RoleName"), odr.ReadString("RoleDescription"));
                     AppUser user = new AppUser(odr.ReadInt("Id"), odr.ReadString("Email"), odr.ReadString("Lastname"), odr.ReadString("Surname"), odr.ReadString("FacebookAccessToken"), userRole);
                     users.Add(user);
                  }
               }
            }
         }
         return users;
      }

      public async Task<AppUser> GetOne(string email)
      {
         AppUser user = new AppUser();
         using (var con = context.GetConnection())
         {
            string cmdStr = @"select Id, Email, Lastname, Surname, FacebookAccessToken, RoleId, RoleName, RoleDescription from V_USER where Email=@Email";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Email", email));
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  if (await odr.ReadAsync())
                  {
                     Role userRole = new Role(odr.ReadInt("RoleId"), odr.ReadString("RoleName"), odr.ReadString("RoleDescription"));
                     user = new AppUser(odr.ReadInt("Id"), odr.ReadString("Email"), odr.ReadString("Lastname"), odr.ReadString("Surname"), odr.ReadString("FacebookAccessToken"), userRole);
                  }
               }
            }
         }
         return user;
      }
   }
}
