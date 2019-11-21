using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface IProductRepository
   {
      Task<List<Product>> GetAll();
      Task<Product> GetOne(long id);
      Task<long> Create(Product product);
      Task Update(Product product);
      Task Delete(long id);
      Task<bool> Exists(long id);
      Task<bool> Exists(string name);
      Task<List<Size>> Sizes();
      Task<List<Product>> AssignableProducts(long categoryId);
   }

   public class ProductRepository : IProductRepository
   {
      private readonly Pho84SnackContext context;

      public ProductRepository(Pho84SnackContext context)
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
                     products.Add(new Product(odr.ReadInt("Id"), odr.ReadString("Name"), odr.ReadString("Description")));
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

      public async Task<bool> Exists(string name)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from PRODUCT where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
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
                     products.Add(new Product(odr.ReadInt("Id"), odr.ReadString("Name"), odr.ReadString("Description")));
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
            string cmdStr = "select p.Id, p.Name, p.Description, m.Id as ProductSizeId, m.Price, s.Id as SizeId, s.ShortName, s.LongName from PRODUCT p left join PRODUCTSIZE m on p.Id=m.ProductId left join SIZE s on m.SizeId=s.Id where p.Id=@Id";
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
                        product = new Product(odr.ReadInt("Id"), odr.ReadString("Name"), odr.ReadString("Description"));
                     }
                     Size size = new Size(odr.ReadInt("SizeId"), odr.ReadString("ShortName"), odr.ReadString("LongName"));
                     ProductSize productSize = new ProductSize(odr.ReadInt("ProductSizeId"), product, size, odr.ReadDecimal("Price"));
                     if (size.Id > 0 && productSize.Id > 0)
                     {
                        product.ProductSizes.Add(productSize);
                     }
                  }
               }
            }
         }
         return product;
      }

      public async Task<List<Size>> Sizes()
      {
         List<Size> availableSizes = new List<Size>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, ShortName, LongName from SIZE";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     availableSizes.Add(new Size(odr.ReadInt("Id"), odr.ReadString("ShortName"), odr.ReadString("LongName")));
                  }
               }
            }
         }
         return availableSizes;
      }

      public async Task Delete(long id)
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
