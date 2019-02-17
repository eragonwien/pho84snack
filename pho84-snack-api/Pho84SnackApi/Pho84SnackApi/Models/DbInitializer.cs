using System;
using System.Linq;
using Pho84SnackApi.Services;

namespace Pho84SnackApi.Models
{
    public class DbInitializer
    {
        public static void Initalize(PHO84SNACKContext context)
        {
            if (context.Restaurant.Any())
            {
                return;
            }

            CreateRestaurant(context);

            CreateRoles(context);
            CreateUser(context);

            CreateContact(context);
            CreateOpenHours(context);

            CreateCategories(context);
            CreateProducts(context);
            CreateMenu(context);
            BindMenuProducts(context);
        }

        private static void BindMenuProducts(PHO84SNACKContext context)
        {
            Menu menu = context.Menu.First();
            Product product = context.Product.First();
            MenuProduct menuProduct = new MenuProduct
            {
                IsActive = true,
                MenuId = menu.Id,
                ProductId = product.Id,
                Description = product.Description,
                Name = product.Name,
                Currency = product.Currency,
                Price = product.Price
            };
            context.MenuProduct.Add(menuProduct);
            context.SaveChanges();
        }

        private static void CreateMenu(PHO84SNACKContext context)
        {
            Menu menu = new Menu
            {
                IsActive = true,
                Name = "Breakfast TEST Menu",
                Price = 100,
                Currency = Currency.EUR.ToString(),
                RestaurantId = context.Restaurant.First().Id
            };
            context.Menu.Add(menu);
            context.SaveChanges();
        }

        private static void CreateProducts(PHO84SNACKContext context)
        {
            Product product = new Product
            {
                IsActive = true,
                CategoryId = context.Category.First().Id,
                Name = "TEST PRODUCT",
                Price = 99,
                Description = "This is a TEST PRODUCT",
                Currency = Currency.EUR.ToString()
            };
            context.Product.Add(product);
            context.SaveChanges();
        }

        private static void CreateCategories(PHO84SNACKContext context)
        {
            Category category = new Category
            {
                IsActive = true,
                Name = "TEST CATEGORY",
                RestaurantId = context.Restaurant.First().Id
            };
            context.Category.Add(category);
            context.SaveChanges();
        }

        private static void CreateOpenHours(PHO84SNACKContext context)
        {
            Contact contact = context.Contact.First();
            var openHours = new OpenHour[]
           {
                new OpenHour() { Day = "Mo.", IsOpen = true, Open = "12:00", Close = "19:00", ContactId = contact.Id },
                new OpenHour() { Day = "Di. - Fr.", IsOpen = true, Open = "09:00", Close = "19:00", ContactId = contact.Id },
                new OpenHour() { Day = "Sa.", IsOpen = true, Open = "09:00", Close = "16:00", ContactId = contact.Id },
                new OpenHour() { Day = "So.", IsOpen = false, ContactId = contact.Id },
                new OpenHour() { Day = "Feiertag", IsOpen = false, ContactId = contact.Id }
           };
            foreach (var openHour in openHours)
            {
                context.OpenHour.Add(openHour);
            }
            context.SaveChanges();
        }

        private static void CreateContact(PHO84SNACKContext context)
        {
            Contact contact = new Contact
            {
                IsActive = true,
                Address1 = "Neubaugasse 78",
                City = "Wien",
                Plz = "1070",
                RestaurantId = context.Restaurant.First().Id,
            };
            context.Add(contact);
            context.SaveChanges();
        }

        private static void CreateUser(PHO84SNACKContext context)
        {
            User user = new User
            {
                IsActive = true,
                Email = "test@email",
                Name = "Tester",
                Password = SnackCore.GetEncodedPassword("1234"),
                RoleId = context.Role.First().Id
            };
            context.User.Add(user);
            context.SaveChanges();
        }

        private static void CreateRoles(PHO84SNACKContext context)
        {
            Role admin = new Role
            {
                IsActive = true,
                Title = "Admin",
                Description = "can change settings"
            };
            context.Role.Add(admin);
            context.SaveChanges();
            
        }

        private static void CreateRestaurant(PHO84SNACKContext context)
        {
            Restaurant restaurant = new Restaurant
            {
                IsActive = true,
                Name = "Test Snack",
                WelcomeText = "Welcome Text Snack",
                WelcomeTitle = "Welcome Title"
            };
            context.Restaurant.Add(restaurant);
            context.SaveChanges();
        }
    }
}
