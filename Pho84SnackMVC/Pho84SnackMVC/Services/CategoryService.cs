using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pho84SnackMVC.Services
{
   public interface ICategoryService
   {
      List<Category> GetAll();
      Category GetOne(int id);
      Category GetOne(string name);
      long Create(Category category);
      void Update(CategoryEditViewModel model);
      void Remove(int id);
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
         Category category = null;
         using (var con = context.GetConnection())
         {
            con.Open();
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
                        category = new Category(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id"));
                     }
                     if (!odr.IsDBNull(odr.GetOrdinal("ProductId")))
                     {
                        category.Products.Add(new Product(odr.GetString("ProductName"), odr.GetString("ProductDescription"), odr.GetInt32("ProductId")));
                     }
                  }
               }
            }
         }
         return category;
      }

      public Category GetOne(string name)
      {
         Category category = null;
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description, ProductId, ProductName, ProductDescription from V_CATEGORY where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     if (category == null)
                     {
                        category = new Category(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id"));
                     }
                     category.Products.Add(new Product(odr.GetString("ProductName"), odr.GetString("ProductDescription"), odr.GetInt32("ProductId")));
                  }
               }
            }
         }
         return category;
      }

      public void Remove(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
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

      public void Update(CategoryEditViewModel model)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            var transaction = con.BeginTransaction();
            try
            {
               // Name & Beschreibung aktualisieren
               string updateInfoCmdStr = "update CATEGORY set Name=@Name, Description=@Description where Id=@Id";
               using (var cmd = new MySqlCommand(updateInfoCmdStr, con, transaction))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Name", model.Name));
                  cmd.Parameters.Add(new MySqlParameter("@Description", model.Description));
                  cmd.Parameters.Add(new MySqlParameter("@Id", model.Id));
                  cmd.ExecuteNonQuery();
               }

               // Unreferenzierte Produkte entfernen
               string removeProductCmdStr = @"delete from PRODUCTMAP where CategoryId=@Id";
               if (model.ProductIds.Count > 0)
               {
                  removeProductCmdStr += string.Format(" and ProductId not in ({0})", string.Join(",", model.ProductIds));
               }
               using (var cmd = new MySqlCommand(removeProductCmdStr, con, transaction))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", model.Id));
                  cmd.ExecuteNonQuery();
               }

               // Neue Produkte filtern
               List<int> newProductIds = model.ProductIds.Select(i => Convert.ToInt32(i)).ToList();
               string selectedProductCmdStr = "select ProductId from PRODUCTMAP where CategoryId=@Id";
               using (var cmd = new MySqlCommand(selectedProductCmdStr, con, transaction))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", model.Id));
                  using (var odr = cmd.ExecuteReader())
                  {
                     while (odr.Read())
                     {
                        int productId = odr.GetInt32("ProductId");
                        newProductIds.RemoveAll(i => i == productId);
                     }
                  }
               }

               // neue Produkte hinzufügen
               foreach (var productId in newProductIds)
               {
                  string addProductCmdStr = @"insert into PRODUCTMAP(CategoryId, ProductId) values(@CategoryId, @ProductId) on duplicate key update Id=Id";
                  using (var cmd = new MySqlCommand(addProductCmdStr, con))
                  {
                     cmd.Parameters.Add(new MySqlParameter("@CategoryId", model.Id));
                     cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
                     cmd.ExecuteNonQuery();
                  }
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
   }
}
