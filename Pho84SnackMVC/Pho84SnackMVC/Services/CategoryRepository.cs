using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface ICategoryRepository
   {
      Task<List<Category>> GetAll();
      Task<Category> GetOne(long id);
      Task<long> Create(Category category);
      Task Update(Category category);
      Task Patch(long id, string property, string value);
      Task Delete(long id);
      Task<bool> Exists(long id);
      Task<bool> Exists(string name);
      Task Assign(long categoryId, long productId);
      Task RemoveAssigned(long categoryId, IEnumerable<long> assignedIds);
      Task Unassign(long categoryId, long productId);
   }

   public class CategoryRepository : ICategoryRepository
   {
      private readonly Pho84SnackContext context;

      public CategoryRepository(Pho84SnackContext context)
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
                     if (!odr.IsDbNull("ProductId"))
                     {
                        int productId = odr.ReadInt32("ProductId");
                        category.Products.Add(new Product(odr.ReadString("ProductName"), odr.ReadString("ProductDescription"), productId));
                        category.SelectedProductIds.Add(productId);
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

      public async Task Delete(long id)
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

      private async Task<bool> AssignmentExists(long categoryId, long productId)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from PRODUCTMAP where CategoryId=@CategoryId and ProductId=@ProductId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@CategoryId", categoryId));
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

      public async Task Unassign(long categoryId, long productId)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "delete from PRODUCTMAP where CategoryId=@CategoryId and ProductId=@ProductId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@CategoryId", categoryId));
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      private async Task UnassignProducts(Category category)
      {

         using (var con = context.GetConnection())
         {
            string cmdStr = "delete from PRODUCTMAP where CategoryId=@CategoryId";
            if (category.SelectedProductIds.Count > 0)
            {
               cmdStr += string.Format(" and ProductId not in ({0})", string.Join(",", category.SelectedProductIds));
            }
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@CategoryId", category.Id));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      private async Task AssignNewProducts(Category category)
      {
         foreach (long productId in category.SelectedProductIds)
         {
            if (!await AssignmentExists(category.Id, productId))
            {
               await Assign(category.Id, productId);
            }
         }
      }

      public async Task<bool> Exists(string name)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from CATEGORY where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               await con.OpenAsync();
               return await cmd.ReadScalarInt32() > 0;
            }
         }
      }

      public async Task Update(Category category)
      {
         await UpdateInfo(category);
         await UpdateProducts(category);
      }

      private async Task UpdateInfo(Category category)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "update CATEGORY set Name=@Name, Description=@Description where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", category.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", category.Description));
               cmd.Parameters.Add(new MySqlParameter("@Id", category.Id));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      private async Task UpdateProducts(Category category)
      {
         await AssignNewProducts(category);
         await UnassignProducts(category);
      }
   }
}
