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
      Task<bool> Exists(long id);
      Task<bool> Exists(string email);
      Task Create(AppUser user);
      Task<AppUser> GetOne(long id);
      Task<AppUser> GetOne(string email);
      Task<List<AppUser>> GetAll();
      Task Update(AppUser appUser);
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
            string cmdStr = @"insert into APPUSER(Email, Lastname, Firstname, FacebookAccessToken, Active) values(@Email, @Lastname, @Firstname, @FacebookAccessToken, @Active)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Email", user.Email));
               cmd.Parameters.Add(new MySqlParameter("@Lastname", user.Lastname));
               cmd.Parameters.Add(new MySqlParameter("@Firstname", user.Firstname));
               cmd.Parameters.Add(new MySqlParameter("@FacebookAccessToken", user.FacebookAccessToken));
               cmd.Parameters.Add(new MySqlParameter("@Active", user.Active ? 1 : 0));
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

      public async Task<bool> Exists(long id)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = @"select count(*) from APPUSER where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
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
            string cmdStr = @"select Id, Email, Lastname, Firstname, FacebookAccessToken, Active from APPUSER";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     AppUser user = new AppUser(odr.ReadInt("Id"), odr.ReadString("Email"), odr.ReadString("Lastname"), odr.ReadString("Firstname"), odr.ReadString("FacebookAccessToken"), odr.ReadBoolean("Active"));
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
            string cmdStr = @"select Id, Email, Lastname, Firstname, FacebookAccessToken, Active from APPUSER where Email=@Email";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Email", email));
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  if (await odr.ReadAsync())
                  {
                     user = new AppUser(odr.ReadInt("Id"), odr.ReadString("Email"), odr.ReadString("Lastname"), odr.ReadString("Firstname"), odr.ReadString("FacebookAccessToken"), odr.ReadBoolean("Active"));
                  }
               }
            }
         }
         return user;
      }

      public async Task<AppUser> GetOne(long id)
      {
         AppUser user = new AppUser();
         using (var con = context.GetConnection())
         {
            string cmdStr = @"select Id, Email, Lastname, Firstname, FacebookAccessToken, Active from APPUSER where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  if (await odr.ReadAsync())
                  {
                     user = new AppUser(odr.ReadInt("Id"), odr.ReadString("Email"), odr.ReadString("Lastname"), odr.ReadString("Firstname"), odr.ReadString("FacebookAccessToken"), odr.ReadBoolean("Active"));
                  }
               }
            }
         }
         return user;
      }

      public async Task Update(AppUser user)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = @"update APPUSER set Email=@Email, Lastname=@Lastname, Firstname=@Firstname, Active=@Active where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Email", user.Email));
               cmd.Parameters.Add(new MySqlParameter("@Lastname", user.Lastname));
               cmd.Parameters.Add(new MySqlParameter("@Firstname", user.Firstname));
               cmd.Parameters.Add(new MySqlParameter("@Active", user.Active ? 1 : 0));
               cmd.Parameters.Add(new MySqlParameter("@Id", user.Id));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }
   }
}
