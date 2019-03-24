using System.Collections.Generic;

namespace JsonConverter.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Plz { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public List<string> Directions { get; set; }
    }

    public class OpenHour
    {
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Close { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PriceS { get; set; }
        public decimal PriceM { get; set; }
        public decimal PriceL { get; set; }
        public decimal PriceK { get; set; }
        public string Image { get; set; }
        public bool Featured { get; set; }
    }

    public class Feature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Button { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductDescription { get; set; }
    }

    public class GalleryItem
    {
        public string Image { get; set; }
        public string Text { get; set; }
    }

    public class ConvertResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Content { get; set; }
    }
}
