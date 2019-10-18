using Pho84SnackMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface ICompanyService
   {
      // Category
      Task<IEnumerable<Category>> GetCategories();
      Task<Category> GetCategory(long id);
      Task<long> CreateCategory(Category category);
      Task UpdateCategory(Category category);
      Task DeleteCategory(long id);
      Task<bool> CategoryExist(long id);
      Task<bool> CategoryExist(string name);
      Task UnassignProductFromCategory(long categoryId, long productId);

      // Product
      Task<IEnumerable<Product>> GetProducts();
      
   }

   public class CompanyService : ICompanyService
   {
      private readonly ICompanyInfoRepository companyInfoRepository;
      private readonly ICategoryRepository categoryRepository;
      private readonly IProductRepository productRepository;
      private readonly IPriceRepository priceRepository;

      public CompanyService(ICompanyInfoRepository companyInfoRepository, ICategoryRepository categoryRepository, IProductRepository productRepository, IPriceRepository priceRepository)
      {
         this.companyInfoRepository = companyInfoRepository;
         this.categoryRepository = categoryRepository;
         this.productRepository = productRepository;
         this.priceRepository = priceRepository;
      }

      #region Category

      public async Task<bool> CategoryExist(long id)
      {
         return await categoryRepository.Exists(id);
      }

      public async Task<bool> CategoryExist(string name)
      {
         return await categoryRepository.Exists(name);
      }

      public async Task<long> CreateCategory(Category category)
      {
         return await categoryRepository.Create(category);
      }

      public async Task DeleteCategory(long id)
      {
         await categoryRepository.Delete(id);
      }

      public async Task<IEnumerable<Category>> GetCategories()
      {
         return await categoryRepository.GetAll();
      }

      public async Task<Category> GetCategory(long id)
      {
         return await categoryRepository.GetOne(id);
      }

      public async Task UpdateCategory(Category category)
      {
         await categoryRepository.UpdateInfo(category);
         await categoryRepository.UpdateProducts(category);
      }

      public async Task UnassignProductFromCategory(long categoryId, long productId)
      {
         await categoryRepository.Unassign(categoryId, productId);
      }

      #endregion

      #region Product

      public async Task<IEnumerable<Product>> GetProducts()
      {
         return await productRepository.GetAll();
      }

      #endregion
   }

}
