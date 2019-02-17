using System;
using System.Collections.Generic;

namespace Pho84SnackApi.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Category = new HashSet<Category>();
            Contact = new HashSet<Contact>();
            Menu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string WelcomeTitle { get; set; }
        public string WelcomeText { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Contact> Contact { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
