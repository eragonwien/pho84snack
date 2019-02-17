using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pho84SnackApi.Models;
using Pho84SnackApi.Services;

namespace Pho84SnackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository repository;
        private readonly ILogger<RestaurantController> log;

        public RestaurantController(IRestaurantRepository repository, ILogger<RestaurantController> log)
        {
            this.repository = repository;
            this.log = log;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await repository.GetAll();
            if (restaurants == null || restaurants.Count == 0)
            {
                return NoContent();
            }
            return Ok(restaurants);
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var restaurant = await repository.GetOne(id);
            restaurant = restaurant ?? await repository.GetFirstAvailable();

            if (restaurant == null)
            {
                return NoContent();    
            }

            return Ok(restaurant);
        }

        //// PUT: api/Restaurant/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        //{
        //    if (id != restaurant.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(restaurant).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RestaurantExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Restaurant
        //[HttpPost]
        //public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        //{
        //    _context.Restaurant.Add(restaurant);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        //}

        //// DELETE: api/Restaurant/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Restaurant>> DeleteRestaurant(int id)
        //{
        //    var restaurant = await _context.Restaurant.FindAsync(id);
        //    if (restaurant == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Restaurant.Remove(restaurant);
        //    await _context.SaveChangesAsync();

        //    return restaurant;
        //}

        //private bool RestaurantExists(int id)
        //{
        //    return _context.Restaurant.Any(e => e.Id == id);
        //}
    }
}
