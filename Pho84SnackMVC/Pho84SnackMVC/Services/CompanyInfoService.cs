using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;

namespace Pho84SnackMVC.Services
{
   public interface ICompanyInfoService
   {
      IEnumerable<CompanyInfo> GetAll();
      CompanyInfo GetOne(int id);
      CompanyInfo GetOne(string name);
      long Create(CompanyInfo companyInfo);
      void Update(CompanyInfo companyInfo);
      void Remove(int id);
      void Remove(string name);
      bool Exists(int id);
      bool Exists(string name);
      int Count();
   }

   public class CompanyInfoService : ICompanyInfoService
   {
      private readonly Pho84SnackContext context;

      public CompanyInfoService(Pho84SnackContext context)
      {
         this.context = context;
      }

      public int Count()
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select count(*) from COMPANYINFO";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               return Convert.ToInt32(cmd.ExecuteScalar());
            }
         }
      }

      public long Create(CompanyInfo companyInfo)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "insert into COMPANYINFO(Name, Description, Address, AddressExtra, Zip, City, Phone, Email, Facebook) values(@Name, @Description, @Address, @AddressExtra, @Zip, @City, @Phone, @Email, @Facebook)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", companyInfo.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", companyInfo.Description));
               cmd.Parameters.Add(new MySqlParameter("@Address", companyInfo.Address));
               cmd.Parameters.Add(new MySqlParameter("@AddressExtra", companyInfo.AddressExtra));
               cmd.Parameters.Add(new MySqlParameter("@Zip", companyInfo.Zip));
               cmd.Parameters.Add(new MySqlParameter("@City", companyInfo.City));
               cmd.Parameters.Add(new MySqlParameter("@Phone", companyInfo.Phone));
               cmd.Parameters.Add(new MySqlParameter("@Email", companyInfo.Email));
               cmd.Parameters.Add(new MySqlParameter("@Facebook", companyInfo.Facebook));
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
            string cmdStr = "select count(*) from COMPANYINFO where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
         }
      }

      public bool Exists(string name)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select count(*) from COMPANYINFO where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
         }
      }

      public IEnumerable<CompanyInfo> GetAll()
      {
         List<CompanyInfo> companyInfos = new List<CompanyInfo>();
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description, Address, AddressExtra, Zip, City, Phone, Email, Facebook from COMPANYINFO";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     companyInfos.Add(new CompanyInfo
                     {
                        Id = odr.GetInt32("Id"),
                        Name = odr.GetString("Name"),
                        Description = odr.GetString("Description"),
                        Address = odr.GetString("Address"),
                        AddressExtra = odr.GetString("AddressExtra"),
                        Zip = odr.GetString("Zip"),
                        City = odr.GetString("City"),
                        Phone = odr.GetString("Phone"),
                        Email = odr.GetString("Email"),
                        Facebook = odr.GetString("Facebook")
                     });
                  }
               }
            }
         }
         return companyInfos;
      }

      public CompanyInfo GetOne(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description, Address, AddressExtra, Zip, City, Phone, Email, Facebook from COMPANYINFO where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               using (var odr = cmd.ExecuteReader())
               {
                  if (odr.Read())
                  {
                     return new CompanyInfo
                     {
                        Id = odr.GetInt32("Id"),
                        Name = odr.GetString("Name"),
                        Description = odr.GetString("Description"),
                        Address = odr.GetString("Address"),
                        AddressExtra = odr.GetString("AddressExtra"),
                        Zip = odr.GetString("Zip"),
                        City = odr.GetString("City"),
                        Phone = odr.GetString("Phone"),
                        Email = odr.GetString("Email"),
                        Facebook = odr.GetString("Facebook")
                     };
                  }
               }
            }
         }
         return new CompanyInfo();
      }

      public CompanyInfo GetOne(string name)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "select Id, Name, Description, Address, AddressExtra, Zip, City, Phone, Email, Facebook from COMPANYINFO where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               using (var odr = cmd.ExecuteReader())
               {
                  if (odr.Read())
                  {
                     return new CompanyInfo
                     {
                        Id = odr.GetInt32("Id"),
                        Name = odr.GetString("Name"),
                        Description = odr.GetString("Description"),
                        Address = odr.GetString("Address"),
                        AddressExtra = odr.GetString("AddressExtra"),
                        Zip = odr.GetString("Zip"),
                        City = odr.GetString("City"),
                        Phone = odr.GetString("Phone"),
                        Email = odr.GetString("Email"),
                        Facebook = odr.GetString("Facebook")
                     };
                  }
               }
            }
         }
         return new CompanyInfo();
      }

      public void Remove(int id)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "delete from COMPANYINFO where Id=@Id";
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
            string cmdStr = "delete from COMPANYINFO where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               cmd.ExecuteNonQuery();
            }
         }
      }

      public void Update(CompanyInfo companyInfo)
      {
         using (var con = context.GetConnection())
         {
            con.Open();
            string cmdStr = "update COMPANYINFO set Name=@Name, Description=@Description, Address=@Address, AddressExtra=@AddressExtra, Zip=@Zip, City=@City, Phone=@Phone, Email=@Email, Facebook=@Facebook where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", companyInfo.Name));
               cmd.Parameters.Add(new MySqlParameter("@Description", companyInfo.Description));
               cmd.Parameters.Add(new MySqlParameter("@Address", companyInfo.Address));
               cmd.Parameters.Add(new MySqlParameter("@AddressExtra", companyInfo.AddressExtra));
               cmd.Parameters.Add(new MySqlParameter("@Zip", companyInfo.Zip));
               cmd.Parameters.Add(new MySqlParameter("@City", companyInfo.City));
               cmd.Parameters.Add(new MySqlParameter("@Phone", companyInfo.Phone));
               cmd.Parameters.Add(new MySqlParameter("@Email", companyInfo.Email));
               cmd.Parameters.Add(new MySqlParameter("@Facebook", companyInfo.Facebook));
               cmd.Parameters.Add(new MySqlParameter("@Id", companyInfo.Id));
               cmd.ExecuteNonQuery();
            }
         }
      }
   }
}
