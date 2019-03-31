using System;
using System.Collections.Generic;
using System.Text;

namespace Pho84SnackJsonConverter.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public List<Product> Products { get; set; }
    }
}
