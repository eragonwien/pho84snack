using JsonConverter.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JsonConverter.Services
{
    public class ConvertService : IConvertService
    {
        private const char SPLIT = ';';

        public ConvertResult ConvertFileToJsonAsync(string type, IFormFile file)
        {
            ConvertResult result = new ConvertResult();
            try
            {
                switch (type)
                {
                    case nameof(Contact):
                        result.Content = GetContact(file);
                        break;
                    case nameof(OpenHour):
                        result.Content = GetOpenHour(file);
                        break;
                    case nameof(Category):
                        result.Content = GetCategories(file);
                        break;
                    case nameof(Menu):
                        result.Content = GetMenu(file);
                        break;
                    case nameof(Feature):
                        result.Content = GetFeatures(file);
                        break;
                    case nameof(GalleryItem):
                        result.Content = GetGallery(file);
                        break;
                    default:
                        throw new Exception($"Funktion for {type} not found");
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public ConvertResult SaveToFile(string type, string jsonData, string directory)
        {
            ConvertResult result = new ConvertResult();
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string fileName = GetFileName(type);

                string filePath = Path.Combine(directory, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (FileStream fs = File.Create(filePath))
                {
                    byte[] content = Encoding.UTF8.GetBytes(jsonData);
                    fs.Write(content, 0, content.Length);
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private string GetContact(IFormFile file)
        {
            string result = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var header = reader.ReadLine().Split(SPLIT).ToList();
                var line = reader.ReadLine().Split(SPLIT);
                Contact contact = new Contact
                {
                    Name = line[header.IndexOf(nameof(Contact.Name))],
                    Address1 = line[header.IndexOf(nameof(Contact.Address1))],
                    Address2 = line[header.IndexOf(nameof(Contact.Address2))],
                    Description = line[header.IndexOf(nameof(Contact.Description))],
                    Plz = line[header.IndexOf(nameof(Contact.Plz))],
                    City = line[header.IndexOf(nameof(Contact.City))],
                    Phone = line[header.IndexOf(nameof(Contact.Phone))],
                    Email = line[header.IndexOf(nameof(Contact.Email))],
                    Facebook = line[header.IndexOf(nameof(Contact.Facebook))],
                    FacebookUrl = line[header.IndexOf(nameof(Contact.FacebookUrl))]
                };
                result = JsonSerializeObject(contact);
            }

            return result;
        }

        private string GetOpenHour(IFormFile file)
        {
            string result = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var header = reader.ReadLine().Split(SPLIT).ToList();
                List<OpenHour> openHours = new List<OpenHour>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(SPLIT);

                    OpenHour openHour = new OpenHour
                    {
                        Day = line[header.IndexOf(nameof(OpenHour.Day))],
                        From = line[header.IndexOf(nameof(OpenHour.From))],
                        To = line[header.IndexOf(nameof(OpenHour.To))],
                        Close = line[header.IndexOf(nameof(OpenHour.Close))].ToLower() == "true"
                    };

                    if (!openHours.Any(h => h.Day == openHour.Day))
                    {
                        openHours.Add(openHour);
                    }
                }
                result = JsonSerializeObject(openHours);
            }

            return result;
        }

        private const string MENU_NAME = "MenuName";
        private const string MENU_ALIAS = "MenuAlias";
        private const string MENU_DESCRIPTION = "MenuDescription";
        private const string MENU_IMAGE = "MenuImage";

        private string GetMenu(IFormFile file)
        {
            string result = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var header = reader.ReadLine().Split(SPLIT).ToList();

                List<Menu> menuList = new List<Menu>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(SPLIT);
                    string menuName = line[header.IndexOf(MENU_NAME)];

                    // Create new menu
                    if (!menuList.Any(m => m.Name.Equals(menuName)))
                    {
                        menuList.Add(new Menu
                        {
                            Name = menuName,
                            Alias = line[header.IndexOf(MENU_ALIAS)],
                            Description = line[header.IndexOf(MENU_DESCRIPTION)],
                            Image = line[header.IndexOf(MENU_IMAGE)],
                            Products = new List<Product>()
                        });
                    }

                    // Update menu description
                    Menu menu = menuList.Single(m => m.Name.Equals(menuName));
                    menu.Alias = !string.IsNullOrWhiteSpace(menu.Alias) ? menu.Alias : line[header.IndexOf(MENU_ALIAS)];
                    menu.Description = !string.IsNullOrWhiteSpace(menu.Description) ? menu.Description : line[header.IndexOf(MENU_DESCRIPTION)];
                    menu.Image = !string.IsNullOrWhiteSpace(menu.Image) ? menu.Image : line[header.IndexOf(MENU_IMAGE)];

                    // Add new product
                    Product product = new Product
                    {
                        Name = line[header.IndexOf(nameof(Product.Name))],
                        Alias = line[header.IndexOf(nameof(Product.Alias))],
                        Description = line[header.IndexOf(nameof(Product.Description))],
                        Image = line[header.IndexOf(nameof(Product.Image))],
                        Price = !string.IsNullOrWhiteSpace(line[header.IndexOf(nameof(Product.Price))]) ?
                            Convert.ToDecimal(line[header.IndexOf(nameof(Product.Price))]) : 0
                    };
                    menu.Products.Add(product);
                }
                result = JsonSerializeObject(menuList);
            }

            return result;
        }

        private const string CATEGORY_NAME = "CategoryName";
        private const string CATEGORY_TYPE = "CategoryType";
        private const string CATEGORY_IMAGE = "CategoryImage";

        private string GetCategories(IFormFile file)
        {
            string result = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var header = reader.ReadLine().Split(SPLIT).ToList();

                List<Category> categories = new List<Category>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(SPLIT);
                    string categoryName = line[header.IndexOf(CATEGORY_NAME)];

                    // Create new category
                    if (!categories.Any(m => m.Name.Equals(categoryName)))
                    {
                        categories.Add(new Category
                        {
                            Name = categoryName,
                            Type = line[header.IndexOf(CATEGORY_TYPE)],
                            Image = line[header.IndexOf(CATEGORY_IMAGE)],
                            Products = new List<Product>()
                        });
                    }

                    // Update category description
                    Category category = categories.Single(c => c.Name.Equals(categoryName));
                    category.Type = !string.IsNullOrWhiteSpace(category.Type) ? category.Type : line[header.IndexOf(CATEGORY_TYPE)];
                    category.Image = !string.IsNullOrWhiteSpace(category.Image) ? category.Image : line[header.IndexOf(CATEGORY_IMAGE)];

                    // Add new product
                    Product product = new Product
                    {
                        Name = line[header.IndexOf(nameof(Product.Name))],
                        Alias = line[header.IndexOf(nameof(Product.Alias))],
                        Description = line[header.IndexOf(nameof(Product.Description))],
                        Image = line[header.IndexOf(nameof(Product.Image))],
                        Price = !string.IsNullOrWhiteSpace(line[header.IndexOf(nameof(Product.Price))]) ?
                            Convert.ToDecimal(line[header.IndexOf(nameof(Product.Price))]) : 0
                    };
                    category.Products.Add(product);
                }
                result = JsonSerializeObject(categories);
            }

            return result;
        }

        private string GetFeatures(IFormFile file)
        {
            string result = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var header = reader.ReadLine().Split(SPLIT).ToList();

                List<Feature> features = new List<Feature>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(SPLIT);

                    Feature feature = new Feature
                    {
                        Id = Convert.ToInt32(line[header.IndexOf(nameof(Feature.Id))]),
                        Title = line[header.IndexOf(nameof(Feature.Title))],
                        Subtitle = line[header.IndexOf(nameof(Feature.Subtitle))],
                        Image = line[header.IndexOf(nameof(Feature.Image))],
                        Description = line[header.IndexOf(nameof(Feature.Description))],
                        Button = line[header.IndexOf(nameof(Feature.Button))],
                        Url = line[header.IndexOf(nameof(Feature.Url))]
                    };
                    features.Add(feature);
                }
                result = JsonSerializeObject(features);
            }

            return result;
        }

        private string GetGallery(IFormFile file)
        {
            string result = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var header = reader.ReadLine().Split(SPLIT).ToList();

                List<GalleryItem> gallery = new List<GalleryItem>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(SPLIT);

                    GalleryItem item = new GalleryItem
                    {
                        Image = line[header.IndexOf(nameof(GalleryItem.Image))],
                        Text = line[header.IndexOf(nameof(GalleryItem.Text))]
                    };
                    gallery.Add(item);
                }
                result = JsonSerializeObject(gallery);
            }

            return result;
        }

        public string GetFileName(string type)
        {
            string fileName;
            switch (type)
            {
                case nameof(Contact):
                    fileName = "contact.json";
                    break;
                case nameof(OpenHour):
                    fileName = "open.hour.json";
                    break;
                case nameof(Category):
                    fileName = "category.json";
                    break;
                case nameof(Menu):
                    fileName = "menu.json";
                    break;
                case nameof(Feature):
                    fileName = "features.json";
                    break;
                case nameof(GalleryItem):
                    fileName = "gallery.json";
                    break;
                default:
                    throw new Exception($"Type {type} not found");
            }
            return fileName;
        }

        private string JsonSerializeObject(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new LowercaseContractResolver()
            };
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}

public class LowercaseContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }
}
