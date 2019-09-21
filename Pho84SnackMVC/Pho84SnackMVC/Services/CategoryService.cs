using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface ICategoryService
   {
      List<Category> GetAll();
      Category GetOne(int id);
      Category GetOne(string name);
      long Create(Category category);
      void Update(Category category);
      void Remove(int id);
      void Remove(string name);
      bool Exists(int id);
      bool Exists(string name);
      int Count();
   }

   public class CategoryService : ICategoryService
   {
      private readonly Pho84SnackContext context;

      public CategoryService(Pho84SnackContext context)
      {
         this.context = context;
      }

      public int Count()
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select count(*) from CATEGORY";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               return Convert.ToInt32(cmd.ExecuteScalar());
            }
         }
      }

      public long Create(Category category)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "insert into CATEGORY(Name, Description) values(@Name, @Description)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", category.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", category.Description));
               cmd.ExecuteNonQuery();
               return cmd.LastInsertedId;
            }
         }
      }

      public bool Exists(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select count(*) from CATEGORY where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               return (Convert.ToInt16(cmd.ExecuteScalar())) > 0;
            }
         }
      }

      public bool Exists(string name)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select count(*) from CATEGORY where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               return (Convert.ToInt16(cmd.ExecuteScalar())) > 0;
            }
         }
      }

      public List<Category> GetAll()
      {
         List<Category> categories = new List<Category>();
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description from CATEGORY";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     categories.Add(new Category(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id")));
                  }
               }
            }
         }
         return categories;
      }

      public Category GetOne(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description from CATEGORY where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               using (var odr = cmd.ExecuteReader())
               {
                  if (odr.Read())
                  {
                     return new Category(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id"));
                  }
               }
            }
         }
         return null;
      }

      public Category GetOne(string name)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description from CATEGORY where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               using (var odr = cmd.ExecuteReader())
               {
                  if (odr.Read())
                  {
                     return new Category(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id"));
                  }
               }
            }
         }
         return null;
      }

      public void Remove(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "delete from CATEGORY where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               cmd.ExecuteNonQuery();
            }
         }
      }

      public void Remove(string name)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "delete from CATEGORY where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               cmd.ExecuteNonQuery();
            }
         }
      }

      public void Update(Category category)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "update CATEGORY set Name=@Name, Description=@Description where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", category.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", category.Description));
               cmd.Parameters.Add(new MySqlParameter("@Id", category.Id));
               cmd.ExecuteNonQuery();
            }
         }
      }
   }
}
