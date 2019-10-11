using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface ICategoryService
   {
      Task<List<Category>> GetAll();
      Task<Category> GetOne(long id);
      Task<long> Create(Category category);
      Task Patch(long id, string property, string value);
      Task Remove(long id);
      Task<bool> Exists(long id);
      Task Assign(long categoryId, long productId);
      Task RemoveAssigned(long categoryId, IEnumerable<long> assignedIds);
   }

   public class CategoryService : ICategoryService
   {
      private readonly Pho84SnackContext context;

      public CategoryService(Pho84SnackContext context)
      {
         this.context = context;
      }

      public async Task<List<Category>> GetAll()
      {
         List<Category> categories = new List<Category>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, Name, Description from CATEGORY";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     categories.Add(new Category(odr.ReadString("Name"), odr.ReadString("Description"), odr.ReadInt32("Id")));
                  }
               }
            }
         }
         return categories;
      }

      public async Task<Category> GetOne(long id)
      {
         Category category = null;
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, Name, Description, ProductId, ProductName, ProductDescription from V_CATEGORY where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));

               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (odr.Read())
                  {
                     if (category == null)
                     {
                        category = new Category(odr.ReadString("Name"), odr.ReadString("Description"), odr.ReadInt32("Id"));
                     }
                     if (!odr.IsDBNull(odr.GetOrdinal("ProductId")))
                     {
                        category.Products.Add(new Product(odr.ReadString("ProductName"), odr.ReadString("ProductDescription"), odr.ReadInt32("ProductId")));
                     }
                  }
               }
            }
         }
         return category;
      }

      public async Task<long> Create(Category category)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "insert into CATEGORY(Name, Description) values(@Name, @Description)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", category.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", category.Description));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
               return cmd.LastInsertedId;
            }
         }
      }

      public async Task Patch(long id, string property, string value)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = string.Format("update CATEGORY set {0}=@Value where Id=@Id", property);
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Value", value));
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public async Task<bool> Exists(long id)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from CATEGORY where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               await con.OpenAsync();
               return await cmd.ReadScalarInt32() > 0;
            }
         }
      }

      public async Task Remove(long id)
      {
         using (var con = context.GetConnection())
         {
            var transaction = con.BeginTransaction();

            try
            {
               await con.OpenAsync();
               string deleteMapCmdStr = "delete from PRODUCTMAP where CategoryId=@Id";
               using (var cmd = new MySqlCommand(deleteMapCmdStr, con, transaction))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", id));
                  await cmd.ExecuteNonQueryAsync();
               }

               string deleteCmdStr = "delete from CATEGORY where Id=@Id";
               using (var cmd = new MySqlCommand(deleteCmdStr, con, transaction))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", id));
                  await cmd.ExecuteNonQueryAsync();
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

      public async Task Assign(long categoryId, long productId)
      {
         if (!await AssignmentExists(categoryId, productId))
         {
            using (var con = context.GetConnection())
            {
               string cmdStr = "insert into PRODUCTMAP(CategoryId, ProductId) values(@CategoryId, @ProductId)";
               using (var cmd = new MySqlCommand(cmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@CategoryId", categoryId));
                  cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
                  await con.OpenAsync();
                  await cmd.ExecuteNonQueryAsync();
               }
            }
         }
      }

      private async Task<bool> AssignmentExists(long id, long productId)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from PRODUCTMAP where CategoryId=@CategoryId and ProductId=@ProductId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@CategoryId", id));
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               await con.OpenAsync();
               return await cmd.ReadScalarInt32() > 0;
            }
         }
      }

      public async Task RemoveAssigned(long categoryId, IEnumerable<long> assignedIds)
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
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }
   }
}
