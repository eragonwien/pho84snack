using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JsonConverter.Services
{
    public static class Helper
    {
        public static List<string> ReadAsList(this IFormFile file)
        {
            List<string> content = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    content.Add(reader.ReadLine());
                }
            }
            return content;
        }
    }
}
