using System;
using System.Collections.Generic;
using System.Text;

namespace Pho84SnackJsonConverter.Models
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
}
