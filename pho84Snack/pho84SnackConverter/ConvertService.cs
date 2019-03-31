using Newtonsoft.Json;
using Pho84SnackJsonConverter.Helpers;
using Pho84SnackJsonConverter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pho84SnackJsonConverter
{
    public class ConvertService : IConvertService
    {
        private const char SPLIT = ';';

        public ConvertResult ConvertFileToJsonAsync(string type, List<string> content)
        {
            ConvertResult result = new ConvertResult();
            try
            {
                switch (type)
                {
                    case nameof(Contact):
                        result.Content = GetContact(content);
                        break;
                    case nameof(OpenHour):
                        result.Content = GetOpenHour(content);
                        break;
                    case nameof(Category):
                        result.Content = GetCategories(content);
                        break;
                    case nameof(Feature):
                        result.Content = GetFeatures(content);
                        break;
                    case nameof(GalleryItem):
                        result.Content = GetGallery(content);
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

        private string GetContact(List<string> content)
        {
            string result = string.Empty;

            var header = content.First().Split(SPLIT).ToList();
            var data = content.Last().Split(SPLIT);

            string directionStr = data[header.IndexOf(nameof(Contact.Directions))];
            Contact contact = new Contact
            {
                Name = data[header.IndexOf(nameof(Contact.Name))],
                Address1 = data[header.IndexOf(nameof(Contact.Address1))],
                Address2 = data[header.IndexOf(nameof(Contact.Address2))],
                Description = data[header.IndexOf(nameof(Contact.Description))],
                Plz = data[header.IndexOf(nameof(Contact.Plz))],
                City = data[header.IndexOf(nameof(Contact.City))],
                Phone = data[header.IndexOf(nameof(Contact.Phone))],
                Email = data[header.IndexOf(nameof(Contact.Email))],
                Facebook = data[header.IndexOf(nameof(Contact.Facebook))],
                Directions = !string.IsNullOrWhiteSpace(directionStr) ? directionStr.Split('|').ToList() : new List<string>()
            };
            result = JsonSerializeObject(contact);

            return result;
        }

        private string GetOpenHour(List<string> content)
        {
            string result = string.Empty;
            List<string> header = new List<string>();
            List<OpenHour> openHours = new List<OpenHour>();

            foreach (string line in content)
            {
                var data = line.Split(SPLIT);
                if (content.IndexOf(line) == 0)
                {
                    header = data.ToList();
                    continue;
                }

                OpenHour openHour = new OpenHour
                {
                    Day = data[header.IndexOf(nameof(OpenHour.Day))],
                    From = data[header.IndexOf(nameof(OpenHour.From))],
                    To = data[header.IndexOf(nameof(OpenHour.To))],
                    Close = data[header.IndexOf(nameof(OpenHour.Close))].ToLower() == "true"
                };

                if (!openHours.Any(h => h.Day == openHour.Day))
                {
                    openHours.Add(openHour);
                }
            }
            result = JsonSerializeObject(openHours);

            return result;
        }

        private const string CATEGORY_NAME = "CategoryName";
        private const string CATEGORY_TYPE = "CategoryType";
        private const string CATEGORY_IMAGE = "CategoryImage";

        private string GetCategories(List<string> content)
        {
            string result = string.Empty;
            List<string> header = new List<string>();
            List<Category> categories = new List<Category>();

            foreach (string line in content)
            {
                var data = line.Split(SPLIT);
                if (content.IndexOf(line) == 0)
                {
                    header = data.ToList();
                    continue;
                }

                if (data.All(l => string.IsNullOrWhiteSpace(l)))
                {
                    continue; // empty string
                }

                string categoryName = data[header.IndexOf(CATEGORY_NAME)];

                // Create new category
                if (!categories.Any(m => m.Name.Equals(categoryName)))
                {
                    categories.Add(new Category
                    {
                        Name = categoryName,
                        Type = data[header.IndexOf(CATEGORY_TYPE)],
                        Image = data[header.IndexOf(CATEGORY_IMAGE)],
                        Products = new List<Product>()
                    });
                }

                // Update category description
                Category category = categories.Single(c => c.Name.Equals(categoryName));
                category.Type = !string.IsNullOrWhiteSpace(category.Type) ? category.Type : data[header.IndexOf(CATEGORY_TYPE)];
                category.Image = !string.IsNullOrWhiteSpace(category.Image) ? category.Image : data[header.IndexOf(CATEGORY_IMAGE)];

                // Add new product
                Product product = new Product
                {
                    Name = data[header.IndexOf(nameof(Product.Name))],
                    Alias = data[header.IndexOf(nameof(Product.Alias))],
                    Description = data[header.IndexOf(nameof(Product.Description))],
                    Image = data[header.IndexOf(nameof(Product.Image))],
                    Price = ToDecimal(data[header.IndexOf(nameof(Product.Price))]),
                    PriceS = ToDecimal(data[header.IndexOf(nameof(Product.PriceS))]),
                    PriceM = ToDecimal(data[header.IndexOf(nameof(Product.PriceM))]),
                    PriceL = ToDecimal(data[header.IndexOf(nameof(Product.PriceL))]),
                    PriceK = ToDecimal(data[header.IndexOf(nameof(Product.PriceK))]),
                    Featured = data[header.IndexOf(nameof(Product.PriceK))] == bool.TrueString
                };

                category.Products.Add(product);
                result = JsonSerializeObject(categories);
            }

            return result;
        }

        private string GetFeatures(List<string> content)
        {
            string result = string.Empty;
            List<string> header = new List<string>();
            List<Feature> features = new List<Feature>();

            foreach (string line in content)
            {
                var data = line.Split(SPLIT);
                if (content.IndexOf(line) == 0)
                {
                    header = data.ToList();
                    continue;
                }

                Feature feature = new Feature
                {
                    Id = Convert.ToInt32(data[header.IndexOf(nameof(Feature.Id))]),
                    Title = data[header.IndexOf(nameof(Feature.Title))],
                    Subtitle = data[header.IndexOf(nameof(Feature.Subtitle))],
                    Image = data[header.IndexOf(nameof(Feature.Image))],
                    Description = data[header.IndexOf(nameof(Feature.Description))],
                    Button = data[header.IndexOf(nameof(Feature.Button))],
                    Url = data[header.IndexOf(nameof(Feature.Url))],
                    ProductName = data[header.IndexOf(nameof(Feature.ProductName))],
                    ProductPrice = data[header.IndexOf(nameof(Feature.ProductPrice))],
                    ProductDescription = data[header.IndexOf(nameof(Feature.ProductDescription))]
                };
                features.Add(feature);
            }
            result = JsonSerializeObject(features);

            return result;
        }

        private string GetGallery(List<string> content)
        {
            string result = string.Empty;
            List<string> header = new List<string>();

            List<GalleryItem> gallery = new List<GalleryItem>();

            foreach (string line in content)
            {
                var data = line.Split(SPLIT);

                if (content.IndexOf(line) == 0)
                {
                    header = data.ToList();
                    continue;
                }

                GalleryItem item = new GalleryItem
                {
                    Image = data[header.IndexOf(nameof(GalleryItem.Image))],
                    Text = data[header.IndexOf(nameof(GalleryItem.Text))]
                };
                gallery.Add(item);
            }
            result = JsonSerializeObject(gallery);
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

        private decimal ToDecimal(string str, decimal defaultValue = 0)
        {
            if (!decimal.TryParse(str, out decimal result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}
