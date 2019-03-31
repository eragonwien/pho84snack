using Pho84SnackJsonConverter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pho84SnackJsonConverter
{
    public interface IConvertService
    {
        ConvertResult ConvertFileToJsonAsync(string type, List<string> content);
        ConvertResult SaveToFile(string type, string jsonData, string directory);
        string GetFileName(string type);
    }
}
