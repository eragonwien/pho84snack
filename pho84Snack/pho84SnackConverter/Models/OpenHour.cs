using System;
using System.Collections.Generic;
using System.Text;

namespace Pho84SnackJsonConverter.Models
{
    public class OpenHour
    {
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Close { get; set; }
    }
}
