using System;
using System.Collections.Generic;

namespace Pho84SnackApi.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int? RestaurantId { get; set; }
        public int? ImageId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual Image Image { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
