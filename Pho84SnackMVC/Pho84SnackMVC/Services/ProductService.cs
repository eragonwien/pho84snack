using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Pho84SnackMVC.Services
{
   public interface IProductService
   {
      List<Product> GetAll();
      Product GetOne(int id);
      Product GetOne(string name);
      long Create(Product product);
      void Update(Product product);
      void Remove(int id);
      bool Exists(int id);
      bool Exists(string name);
      int Count();
      long AddPrice(ProductSizeViewModel model);
      void UpdatePrice(ProductSizeViewModel model);
      List<ProductSize> GetProductSizes(int productId);
   }

   public class ProductService : IProductService
   {
      private readonly Pho84SnackContext context;

      public ProductService(Pho84SnackContext context)
      {
         this.context = context;
      }

      public long AddPrice(ProductSizeViewModel model)
      {
         using (var con = context.GetConnection())
         {
            con.Open();

            string cmdStr = "insert into PRODUCTSIZEMAP(ProductId, ProductSizeId, Price) values(@ProductId, @ProductSizeId, @Price)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", model.ProductId));
               cmd.Parameters.Add(new MySqlParameter("@ProductSizeId", model.ProductSizeId));
               cmd.Parameters.Add(new MySqlParameter("@Price", model.Price));
               cmd.ExecuteNonQuery();
               return cmd.LastInsertedId;
            }
         }
      }

      public int Count()
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select count(*) from PRODUCT";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               return Convert.ToInt32(cmd.ExecuteScalar());
            }
         }
      }

      public long Create(Product product)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "insert into PRODUCT(Name, Description) values(@Name, @Description)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", product.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", product.Description));
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
            string cmdStr = "select count(*) from PRODUCT where Id=@Id";
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
            string cmdStr = "select count(*) from PRODUCT where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               return (Convert.ToInt16(cmd.ExecuteScalar())) > 0;
            }
         }
      }

      public List<Product> GetAll()
      {
         List<Product> products = new List<Product>();
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description from PRODUCT";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     products.Add(new Product(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id")));
                  }
               }
            }
         }
         return products;
      }

      public Product GetOne(int id)
      {
         Product product = null;
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select p.Id, p.Name, p.Description, s.ShortName, s.LongName, m.Id as PriceId, m.ProductId, m.ProductSizeId, m.Price from PRODUCT p inner join PRODUCTSIZEMAP m on p.Id=m.ProductId inner join PRODUCTSIZE s on m.ProductSizeId=s.Id where p.Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     if (product == null)
                     {
                        product = new Product(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id"));
                     }
                     ProductPricing pricing = new ProductPricing(odr.GetInt32("PriceId"), odr.GetInt32("ProductId"), odr.GetInt32("ProductSizeId"), odr.GetString("ShortName"), odr.GetString("LongName"), odr.GetDecimal("Price"));
                     product.PriceList.Add(pricing);
                  }
               }
            }
         }
         return product;
      }

      public Product GetOne(string name)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description from PRODUCT where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               using (var odr = cmd.ExecuteReader())
               {
                  if (odr.Read())
                  {
                     return new Product(odr.GetString("Name"), odr.GetString("Description"), odr.GetInt32("Id"));
                  }
               }
            }
         }
         return null;
      }

      public List<ProductSize> GetProductSizes(int productId)
      {
         List<ProductSize> availableSizes = new List<ProductSize>();
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select s.Id, s.ShortName, s.LongName from PRODUCTSIZE s where not exists(select * from PRODUCTSIZEMAP m where m.ProductId=@ProductId and m.ProductSizeId=s.Id)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     availableSizes.Add(new ProductSize(odr.GetString("ShortName"), odr.GetString("LongName"), odr.GetInt32("Id")));
                  }
               }
            }
         }
         return availableSizes;
      }

      public void Remove(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            var transaction = con.BeginTransaction();

            try
            {
               string deleteMappingCmdStr = "delete from PRODUCTMAP where ProductId=@Id";
               using (var cmd = new MySqlCommand(deleteMappingCmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", id));
                  cmd.ExecuteNonQuery();
               }

               string deleteProductCmdStr = "delete from PRODUCT where Id=@Id";
               using (var cmd = new MySqlCommand(deleteProductCmdStr, con))
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

      public void Update(Product product)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "update PRODUCT set Name=@Name, Description=@Description where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", product.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", product.Description));
               cmd.Parameters.Add(new MySqlParameter("@Id", product.Id));
               cmd.ExecuteNonQuery();
            }
         }
      }

      public void UpdatePrice(ProductSizeViewModel model)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "update PRODUCTSIZEMAP set Price=@Price where ProductId=@ProductId and ProductSizeId=@ProductSizeId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Price", model.Price));
               cmd.Parameters.Add(new MySqlParameter("@ProductId", model.ProductId));
               cmd.Parameters.Add(new MySqlParameter("@ProductSizeId", model.ProductSizeId));
               cmd.ExecuteNonQuery();
            }
         }
      }
   }
}
