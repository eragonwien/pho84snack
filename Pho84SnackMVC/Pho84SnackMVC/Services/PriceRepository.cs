using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{

   public interface IPriceRepository
   {
      Task<long> Add(ProductSize productSize);
      Task Price(ProductSize productSize);
      Task Remove(long id);
      Task Update(long productId, List<ProductSize> productSizes);
   }

   public class PriceRepository : IPriceRepository
   {
      private readonly Pho84SnackContext context;

      public PriceRepository(Pho84SnackContext context)
      {
         this.context = context;
      }

      public async Task<long> Add(ProductSize model)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "insert into PRODUCTSIZE(ProductId, SizeId, Price) values(@ProductId, @SizeId, @Price)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", model.Product.Id));
               cmd.Parameters.Add(new MySqlParameter("@SizeId", model.Size.Id));
               cmd.Parameters.Add(new MySqlParameter("@Price", model.Price));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
               return cmd.LastInsertedId;
            }
         }
      }

      public async Task Remove(long id)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "delete from PRODUCTSIZEMAP where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public async Task Price(ProductSize model)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "update PRODUCTSIZE set Price=@Price where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", model.Id));
               cmd.Parameters.Add(new MySqlParameter("@Price", model.Price));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public async Task Update(long productId, List<ProductSize> productSizes)
      {
         
         var usedSizes = await GetUsedSizes(productId);
         if (usedSizes.Count > 0)
         {
            await DeleteUnusedPrices(productId, productSizes);
            await UpdateExistingPrices(productId, productSizes.Where(p => usedSizes.Contains(p.Size.Id)).ToList());
         }
         await AddPrices(productId, productSizes.Where(p => !usedSizes.Contains(p.Size.Id)).ToList());
      }

      private async Task DeleteUnusedPrices(long productId, List<ProductSize> productSizes)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "delete from PRODUCTSIZE where ProductId=@ProductId";
            if (productSizes.Count > 0)
            {
               cmdStr += string.Format(" and SizeId not in ({0})", string.Join(", ", productSizes.Select(s => s.Size.Id)));
            }
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      private async Task UpdateExistingPrices(long productId, List<ProductSize> productSizes)
      {
         if (productSizes.Count == 0)
         {
            return;
         }
         using (var con = context.GetConnection())
         {
            await con.OpenAsync();
            foreach (var entry in productSizes)
            {
               string cmdStr = "update PRODUCTSIZE set Price=@Price where SizeId=@SizeId and ProductId=@ProductId";
               using (var cmd = new MySqlCommand(cmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Price", entry.Price));
                  cmd.Parameters.Add(new MySqlParameter("@SizeId", entry.Size.Id));
                  cmd.Parameters.Add(new MySqlParameter("@ProductId", entry.Product.Id));
                  await cmd.ExecuteNonQueryAsync();
               }
            }
         }
      }

      private async Task AddPrices(long productId, List<ProductSize> productSizes)
      {
         if (productSizes.Count == 0)
         {
            return;
         }
         using (var con = context.GetConnection())
         {
            await con.OpenAsync();
            foreach (var entry in productSizes)
            {
               string cmdStr = "insert into PRODUCTSIZE(ProductId, SizeId, Price) values(@ProductId, @SizeId, @Price)";
               using (var cmd = new MySqlCommand(cmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@ProductId", entry.Product.Id));
                  cmd.Parameters.Add(new MySqlParameter("@SizeId", entry.Size.Id));
                  cmd.Parameters.Add(new MySqlParameter("@Price", entry.Price));
                  await cmd.ExecuteNonQueryAsync();
               }
            }
         }
      }

      private async Task<List<long>> GetUsedSizes(long productId)
      {
         List<long> usedSizes = new List<long>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select SizeId from PRODUCTSIZE where ProductId=@ProductId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               await con.OpenAsync();
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     usedSizes.Add(odr.GetInt64("SizeId"));
                  }
               }
            }
         }
         return usedSizes;
      }
   }
}
