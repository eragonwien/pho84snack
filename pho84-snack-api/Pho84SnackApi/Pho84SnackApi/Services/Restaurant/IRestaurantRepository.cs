using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pho84SnackApi.Models;

namespace Pho84SnackApi.Services
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAll();
        Task<Restaurant> GetOne(int id);
        Task<Restaurant> GetFirstAvailable();
        Task Add(Restaurant restaurant);
        Task Update(Restaurant restaurant);
        Task Remove(int id);
        Task SaveChanges();
        bool Exist(int id);
    }
}
