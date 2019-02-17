using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pho84SnackApi.Models;

namespace Pho84SnackApi.Services
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly PHO84SNACKContext context;

        public RestaurantRepository(PHO84SNACKContext context)
        {
            this.context = context;
        }

        public Task Add(Restaurant restaurant)
        {
            context.Restaurant.Add(restaurant);
            return Task.CompletedTask;
        }

        public bool Exist(int id)
        {
            return context.Restaurant.Any(r => r.Id == id && r.IsActive);
        }

        public Task<List<Restaurant>> GetAll()
        {
            return context.Restaurant.Where(r => r.IsActive).ToListAsync();
        }

        public Task<Restaurant> GetFirstAvailable()
        {
            return context.Restaurant
                .Include(r => r.Category)
                .Include(r => r.Contact).ThenInclude(c => c.OpenHour)
                .Include(r => r.Category).ThenInclude(c => c.Product)
                .Include(r => r.Menu).ThenInclude(m => m.MenuProduct)
                .FirstOrDefaultAsync(r => r.IsActive);
        }

        public Task<Restaurant> GetOne(int id)
        {
            return context.Restaurant
                .Include(r => r.Category)
                .Include(r => r.Contact).ThenInclude(c => c.OpenHour)
                .Include(r => r.Category).ThenInclude(c => c.Product)
                .Include(r => r.Menu).ThenInclude(m => m.MenuProduct)
                .SingleOrDefaultAsync(r => r.IsActive && r.Id == id);
        }

        public async Task Remove(int id)
        {
            Restaurant restaurant = await context.Restaurant.SingleOrDefaultAsync(r => r.Id == id && r.IsActive);
            if (restaurant != null)
            {
                restaurant.IsActive = false;
                await Update(restaurant);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }

        public Task Update(Restaurant restaurant)
        {
            context.Update<Restaurant>(restaurant);
            return Task.CompletedTask;
        }
    }
}
