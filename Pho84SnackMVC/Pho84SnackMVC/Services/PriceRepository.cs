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
      Task<long> Add(PriceViewModel model);
      Task Price(PriceViewModel model);
      Task Remove(long id);
      Task Update(long productId, List<ProductPricing> priceList);
   }

   public class PriceRepository : IPriceRepository
   {
      private readonly Pho84SnackContext context;

      public PriceRepository(Pho84SnackContext context)
      {
         this.context = context;
      }

      public async Task<long> Add(PriceViewModel model)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "insert into PRODUCTSIZEMAP(ProductId, ProductSizeId, Price) values(@ProductId, @ProductSizeId, @Price)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", model.ProductId));
               cmd.Parameters.Add(new MySqlParameter("@ProductSizeId", model.SizeId));
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

      public async Task Price(PriceViewModel model)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "update PRODUCTSIZEMAP set Price=@Price where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", model.Id));
               cmd.Parameters.Add(new MySqlParameter("@Price", model.Price));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public async Task Update(long productId, List<ProductPricing> priceList)
      {
         
         var usedSizes = await GetUsedSizes(productId);
         if (usedSizes.Count > 0)
         {
            await DeleteUnusedPrices(productId, priceList);
            await UpdateExistingPrices(productId, priceList.Where(p => usedSizes.Contains(p.ProductSizeId)).ToList());
         }
         await AddPrices(productId, priceList.Where(p => !usedSizes.Contains(p.ProductSizeId)).ToList());
      }

      private async Task DeleteUnusedPrices(long productId, List<ProductPricing> priceList)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "delete from PRODUCTSIZEMAP where ProductId=@ProductId";
            if (priceList.Count > 0)
            {
               cmdStr += string.Format(" and ProductSizeId not in ({0})", string.Join(", ", priceList.Select(p => p.ProductSizeId)));
            }
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      private async Task UpdateExistingPrices(long productId, List<ProductPricing> priceList)
      {
         if (priceList.Count == 0)
         {
            return;
         }
         using (var con = context.GetConnection())
         {
            await con.OpenAsync();
            foreach (var entry in priceList)
            {
               string cmdStr = "update PRODUCTSIZEMAP set Price=@Price where Id=@Id";
               using (var cmd = new MySqlCommand(cmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@Price", entry.Price));
                  cmd.Parameters.Add(new MySqlParameter("@Id", entry.Id));
                  await cmd.ExecuteNonQueryAsync();
               }
            }
         }
      }

      private async Task AddPrices(long productId, List<ProductPricing> priceList)
      {
         if (priceList.Count == 0)
         {
            return;
         }
         using (var con = context.GetConnection())
         {
            await con.OpenAsync();
            foreach (var entry in priceList)
            {
               string cmdStr = "insert into PRODUCTSIZEMAP(ProductId, ProductSizeId, Price) values(@ProductId, @ProductSizeId, @Price)";
               using (var cmd = new MySqlCommand(cmdStr, con))
               {
                  cmd.Parameters.Add(new MySqlParameter("@ProductId", entry.ProductId));
                  cmd.Parameters.Add(new MySqlParameter("@ProductSizeId", entry.ProductSizeId));
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
            string cmdStr = "select ProductSizeId from PRODUCTSIZEMAP where ProductId=@ProductId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", productId));
               await con.OpenAsync();
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     usedSizes.Add(odr.GetInt64("ProductSizeId"));
                  }
               }
            }
         }
         return usedSizes;
      }
   }
}
