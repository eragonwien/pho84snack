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
        public string FacebookUrl { get; set; }
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

    public class Menu
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }

    public class Feature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Button { get; set; }
        public string ButtonUrl { get; set; }
        public string Image { get; set; }
    }
}
