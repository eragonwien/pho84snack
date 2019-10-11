using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pho84SnackMVC.Services
{
   public interface ICategoryService
   {
      List<Category> GetAll();
      Category GetOne(long id);
      long Create(Category category);
      void Patch(long id, string property, string value);
      void Remove(long id);
      bool Exists(long id);
      void Assign(long categoryId, long productId);
      void RemoveAssigned(long categoryId, IEnumerable<long> assignedIds);
   }

   public class CategoryService : ICategoryService
   {
      private readonly Pho84SnackContext context;

      public CategoryService(Pho84SnackContext context)
      {
         this.context = context;
      }

      public List<Category> GetAll()
      {
         List<Category> categories = new List<Category>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, Name, Description from CATEGORY";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     categories.Add(new Category(odr.ReadString("Name"), odr.ReadString("Description"), odr.GetInt32("Id")));
                  }
               }
            }
         }
         return categories;
      }

      public Category GetOne(long id)
      {
         Category category = null;
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, Name, Description, ProductId, ProductName, ProductDescription from V_CATEGORY where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     if (category == null)
                     {
                        category = new Category(odr.ReadString("Name"), odr.ReadString("Description"), odr.GetInt32("Id"));
                     }
                     if (!odr.IsDBNull(odr.GetOrdinal("ProductId")))
                     {
                        category.Products.Add(new Product(odr.ReadString("ProductName"), odr.ReadString("ProductDescription"), odr.GetInt32("ProductId")));
                     }
                  }
               }
            }
         }
         return category;
      }

      public long Create(Category category)
      {
         using (var con = context.GetConnection())
         {
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

      public void Patch(long id, string property, string value)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = string.Format("update CATEGORY set {0}=@Value where Id=@Id", property);
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Value", value));
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               cmd.ExecuteNonQuery();
            }
         }
      }

      public bool Exists(long id)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from CATEGORY where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               return (Convert.ToInt16(cmd.ExecuteScalar())) > 0;
            }
         }
      }

      public void Remove(long id)
      {
         using (var con = context.GetConnection())
         {
            var transaction = con.BeginTransaction();

            try
            {
               string deleteMapCmdStr = "delete from PRODUCTMAP where CategoryId=@Id";
               using (var cmd = new MySqlCommand(deleteMapCmdStr, con, transaction))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", id));
                  cmd.ExecuteNonQuery();
               }

               string deleteCmdStr = "delete from CATEGORY where Id=@Id";
               using (var cmd = new MySqlCommand(deleteCmdStr, con, transaction))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", id));
                  cmd.ExecuteNonQuery();
               }

               transaction.Commit();
            }
            catch (Exception)
            {
               transaction.Rollback();
               throw;
            }
         }
      }

      public void Assign(long categoryId, long productId)
      {
         if (!AssignmentExists(categoryId, productId))
         {
            using (var con = context.GetConnection())
            {
               string cmdStr = "insert into PRODUCTMAP(CategoryId, ProductId) values(@CategoryId, @ProductId)";
               using (var cmd = new MySqlCommand(cmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@CategoryId", categoryId));
                  cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
                  cmd.ExecuteNonQuery();
               }
            }
         }
      }

      private bool AssignmentExists(long id, long productId)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from PRODUCTMAP where CategoryId=@CategoryId and ProductId=@ProductId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@CategoryId", id));
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               return cmd.ReadScalarInt32() > 0;
            }
         }
      }

      public void RemoveAssigned(long categoryId, IEnumerable<long> assignedIds)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = @"delete from PRODUCTMAP where CategoryId=@CategoryId";
            if (assignedIds.Count() > 0)
            {
               cmdStr += string.Format(" and ProductId not in ({0})", string.Join(", ", assignedIds));
            }
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@CategoryId", categoryId));
               cmd.ExecuteNonQuery();
            }
         }
      }
   }
}
