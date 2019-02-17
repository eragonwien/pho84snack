using System;
using System.Collections.Generic;

namespace Pho84SnackApi.Models
{
    public partial class MenuProduct
    {
        public int MenuId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Product Product { get; set; }
    }
}
