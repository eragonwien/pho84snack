using System;
using System.Collections.Generic;
using System.Text;

namespace Pho84SnackJsonConverter.Models
{
    public class ConvertResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Content { get; set; }
    }
}
