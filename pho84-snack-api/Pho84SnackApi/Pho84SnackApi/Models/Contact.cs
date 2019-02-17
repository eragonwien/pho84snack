using System;
using System.Collections.Generic;

namespace Pho84SnackApi.Models
{
    public partial class Contact
    {
        public Contact()
        {
            OpenHour = new HashSet<OpenHour>();
        }

        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Plz { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<OpenHour> OpenHour { get; set; }
    }
}
