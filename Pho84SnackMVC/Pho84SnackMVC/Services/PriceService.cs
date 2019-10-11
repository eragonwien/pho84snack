using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{

   public interface IPriceService
   {
      Task<long> Add(PriceViewModel model);
      Task Price(long id, PriceViewModel model);
      Task Remove(long id);
   }

   public class PriceService : IPriceService
   {
      private readonly Pho84SnackContext context;

      public PriceService(Pho84SnackContext context)
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

      public async Task Price(long id, PriceViewModel model)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "update PRODUCTSIZEMAP set Price=@Price where ProductId=@ProductId and ProductSizeId=@ProductSizeId";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@ProductId", model.ProductId));
               cmd.Parameters.Add(new MySqlParameter("@ProductSizeId", model.SizeId));
               cmd.Parameters.Add(new MySqlParameter("@Price", model.Price));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }
   }
}
