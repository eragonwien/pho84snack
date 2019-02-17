using System;
using System.Collections.Generic;

namespace Pho84SnackApi.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuProduct = new HashSet<MenuProduct>();
        }

        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int? ImageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }

        public virtual Image Image { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<MenuProduct> MenuProduct { get; set; }
    }
}
