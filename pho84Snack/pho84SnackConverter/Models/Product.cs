using System;
using System.Collections.Generic;
using System.Text;

namespace Pho84SnackJsonConverter.Models
{
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
}
