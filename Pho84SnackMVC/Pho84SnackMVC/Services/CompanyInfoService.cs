using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;

namespace Pho84SnackMVC.Services
{
   public interface ICompanyInfoService
   {
      List<CompanyInfo> GetAll();
      CompanyInfo GetOne(long id);
      CompanyInfo GetOne(string name);
      long Create(CompanyInfo companyInfo);
      void Update(CompanyInfo companyInfo);
      void Remove(long id);
      void Remove(string name);
      bool Exists(long id);
      bool Exists(string name);
      long Count();
   }

   public class CompanyInfoService : ICompanyInfoService
   {
      private readonly Pho84SnackContext context;

      public CompanyInfoService(Pho84SnackContext context)
      {
         this.context = context;
      }

      public long Count()
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from COMPANYINFO";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               return Convert.ToInt64(cmd.ExecuteScalar());
            }
         }
      }

      public long Create(CompanyInfo companyInfo)
      {
         using (var con = context.GetConnection())
         {
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

      public bool Exists(long id)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from COMPANYINFO where Id=@Id";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Id", id));
               return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
            }
         }
      }

      public bool Exists(string name)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from COMPANYINFO where Name=@Name";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@Name", name));
               return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
            }
         }
      }

      public List<CompanyInfo> GetAll()
      {
         List<CompanyInfo> companyInfos = new List<CompanyInfo>();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, Name, Description, Address, AddressExtra, Zip, City, Phone, Email, Facebook from COMPANYINFO";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               using (var odr = cmd.ExecuteReader())
               {
                  while (odr.Read())
                  {
                     companyInfos.Add(new CompanyInfo
                     {
                        Id = odr.GetInt64("Id"),
                        Name = odr.ReadString("Name"),
                        Description = odr.ReadString("Description"),
                        Address = odr.ReadString("Address"),
                        AddressExtra = odr.ReadString("AddressExtra"),
                        Zip = odr.ReadString("Zip"),
                        City = odr.ReadString("City"),
                        Phone = odr.ReadString("Phone"),
                        Email = odr.ReadString("Email"),
                        Facebook = odr.ReadString("Facebook")
                     });
                  }
               }
            }
         }
         return companyInfos;
      }

      public CompanyInfo GetOne(long id)
      {
         using (var con = context.GetConnection())
         {
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
                        Id = odr.GetInt64("Id"),
                        Name = odr.ReadString("Name"),
                        Description = odr.ReadString("Description"),
                        Address = odr.ReadString("Address"),
                        AddressExtra = odr.ReadString("AddressExtra"),
                        Zip = odr.ReadString("Zip"),
                        City = odr.ReadString("City"),
                        Phone = odr.ReadString("Phone"),
                        Email = odr.ReadString("Email"),
                        Facebook = odr.ReadString("Facebook")
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
                        Id = odr.GetInt64("Id"),
                        Name = odr.ReadString("Name"),
                        Description = odr.ReadString("Description"),
                        Address = odr.ReadString("Address"),
                        AddressExtra = odr.ReadString("AddressExtra"),
                        Zip = odr.ReadString("Zip"),
                        City = odr.ReadString("City"),
                        Phone = odr.ReadString("Phone"),
                        Email = odr.ReadString("Email"),
                        Facebook = odr.ReadString("Facebook")
                     };
                  }
               }
            }
         }
         return new CompanyInfo();
      }

      public void Remove(long id)
      {
         using (var con = context.GetConnection())
         {
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
