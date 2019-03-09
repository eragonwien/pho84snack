using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonConverter.Models;

namespace JsonConverter.Services
{
    public interface IConvertService
    {
        string ConvertFileToJsonAsync(string type, IFormFile file);
        string SaveToFile(string type, string jsonData, string directory);
    }
}
