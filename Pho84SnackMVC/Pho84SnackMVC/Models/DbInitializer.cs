using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class DbInitializer
   {
      public static void Initialize(ICompanyInfoService companyInfoService, ICategoryService categoryService, IProductService productService)
      {
         if (companyInfoService.Count() == 0)
         {
            companyInfoService.Create(new CompanyInfo
            {
               Address = "Test Address 12",
               AddressExtra = string.Empty,
               City = "Test City",
               Description = "Test Description",
               Email = "TestEmail@email.com",
               Facebook = "Test Facebook",
               Name = "Test Info",
               Phone = "+123456789",
               Zip = "12345"
            });
         }

         if (categoryService.Count() == 0)
         {
            categoryService.Create(new Category("Test category", "A test category"));
         }

         if (productService.Count() == 0)
         {
            productService.Create(new Product("Test roduct", "A test product"));
         }
      }
   }
}
