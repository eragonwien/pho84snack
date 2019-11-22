using MySql.Data.MySqlClient;
using Pho84SnackMVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface ICompanyRepository
   {
      Task<Company> Get();
      Task Create(Company companyInfo);
      Task Update(Company companyInfo);
      Task<bool> Exists();
   }

   public class CompanyRepository : ICompanyRepository
   {
      private readonly Pho84SnackContext context;

      public CompanyRepository(Pho84SnackContext context)
      {
         this.context = context;
      }

      public async Task Create(Company companyInfo)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "insert into COMPANY(Name, Description, Address, AddressExtra, Zip, City, Phone, Email, Facebook) values(@Name, @Description, @Address, @AddressExtra, @Zip, @City, @Phone, @Email, @Facebook)";
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
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public async Task<bool> Exists()
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = "select count(*) from COMPANY";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               return await cmd.ReadScalarInt32() > 0;
            }
         }
      }

      public async Task<Company> Get()
      {
         Company company = new Company();
         using (var con = context.GetConnection())
         {
            string cmdStr = "select Id, Name, Description, Address, AddressExtra, Zip, City, Phone, Email, Facebook from COMPANY";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = cmd.ExecuteReader())
               {
                  if (await odr.ReadAsync())
                  {
                     company = new Company
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
         return company;
      }

      public async Task Update(Company companyInfo)
      {
         using (var con = context.GetConnection())
         {
            string cmdStr = @"update COMPANYINFO set Name=@Name, Description=@Description, Address=@Address, AddressExtra=@AddressExtra, Zip=@Zip, City=@City, Phone=@Phone, Email=@Email, Facebook=@Facebook where Id=@Id";
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
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }
   }
}
