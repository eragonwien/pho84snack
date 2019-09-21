using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      void Remove(string name);
      bool Exists(int id);
      bool Exists(string name);
      int Count();
   }

   public class ProductService : IProductService
   {
      private readonly Pho84SnackContext context;

      public ProductService(Pho84SnackContext context)
      {
         this.context = context;
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
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description from PRODUCT where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
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

      public void Remove(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "delete from PRODUCT where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               cmd.ExecuteNonQuery();
            }
         }
      }

      public void Remove(string name)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "delete from PRODUCT where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               cmd.ExecuteNonQuery();
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
   }
}
