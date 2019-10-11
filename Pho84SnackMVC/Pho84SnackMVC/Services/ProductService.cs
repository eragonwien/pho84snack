using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface IProductService
   {
      Task<List<Product>> GetAll();
      Task<Product> GetOne(long id);
      Task<long> Create(Product product);
      Task Update(Product product);
      Task Remove(long id);
      Task<bool> Exists(long id);
      Task<List<ProductSize>> UnassignedSizes(long productId);
      Task<List<Product>> AssignableProducts(long categoryId);
   }

   public class ProductService : IProductService
   {
      private readonly Pho84SnackContext context;

      public ProductService(Pho84SnackContext context)
      {
         this.context = context;
      }

      public async Task<List<Product>> AssignableProducts(long categoryId)
      {
         List<Product> products = new List<Product>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select p.Id, p.Name, p.Description from PRODUCT p where not exists(select * from PRODUCTMAP m where m.CategoryId=@CategoryId and m.ProductId=p.Id)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@CategoryId", categoryId));
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     products.Add(new Product(odr.ReadString("Name"), odr.ReadString("Description"), odr.ReadInt32("Id")));
                  }
               }
            }
         }
         return products;
      }

      public async Task<long> Create(Product product)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "insert into PRODUCT(Name, Description) values(@Name, @Description)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", product.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", product.Description));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
               return cmd.LastInsertedId;
            }
         }
      }

      public async Task<bool> Exists(long id)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from PRODUCT where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               await con.OpenAsync();
               return await cmd.ReadScalarInt32() > 0;
            }
         }
      }

      public async Task<List<Product>> GetAll()
      {
         List<Product> products = new List<Product>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, Name, Description from PRODUCT";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     products.Add(new Product(odr.ReadString("Name"), odr.ReadString("Description"), odr.ReadInt32("Id")));
                  }
               }
            }
         }
         return products;
      }

      public async Task<Product> GetOne(long id)
      {
         Product product = null;
         using (var con = context.GetConnection())
         {
            string cmdStr = "select p.Id, p.Name, p.Description, s.ShortName, s.LongName, m.Id as PriceId, m.ProductId, m.ProductSizeId, m.Price from PRODUCT p left join PRODUCTSIZEMAP m on p.Id=m.ProductId left join PRODUCTSIZE s on m.ProductSizeId=s.Id where p.Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     if (product == null)
                     {
                        product = new Product(odr.ReadString("Name"), odr.ReadString("Description"), odr.ReadInt32("Id"));
                     }
                     ProductPricing pricing = new ProductPricing(odr.ReadInt32("PriceId"), odr.ReadInt32("ProductId"), odr.ReadInt32("ProductSizeId"), odr.ReadString("ShortName"), odr.ReadString("LongName"), odr.ReadDecimal("Price"));
                     if (pricing.Id > 0)
                     {
                        product.PriceList.Add(pricing);
                     }
                  }
               }
            }
         }
         return product;
      }

      public async Task<List<ProductSize>> UnassignedSizes(long productId)
      {
         List<ProductSize> availableSizes = new List<ProductSize>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select s.Id, s.ShortName, s.LongName from PRODUCTSIZE s where not exists(select * from PRODUCTSIZEMAP m where m.ProductId=@ProductId and m.ProductSizeId=s.Id)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     availableSizes.Add(new ProductSize(odr.ReadString("ShortName"), odr.ReadString("LongName"), odr.ReadInt32("Id")));
                  }
               }
            }
         }
         return availableSizes;
      }

      public async Task Remove(long id)
      {
         using (var con = context.GetConnection())
         {
            var transaction = con.BeginTransaction();
            try
            {
               await con.OpenAsync();
               string deleteMappingCmdStr = "delete from PRODUCTMAP where ProductId=@Id";
               using (var cmd = new MySqlCommand(deleteMappingCmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Id", id));
                  await cmd.ExecuteNonQueryAsync();
               }

               string deleteProductCmdStr = "delete from PRODUCT where Id=@Id";
               using (var cmd = new MySqlCommand(deleteProductCmdStr, con))
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

      public async Task Update(Product product)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "update PRODUCT set Name=@Name, Description=@Description where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", product.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", product.Description));
               cmd.Parameters.Add(new MySqlParameter("@Id", product.Id));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }
   }
}
