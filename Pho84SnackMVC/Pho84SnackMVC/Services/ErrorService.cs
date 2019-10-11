using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public interface IErrorService
   {
      Tuple<string, string> HandleException(MySqlException exception);
   }

   public class ErrorService : IErrorService
   {
      public Tuple<string, string> HandleException(MySqlException exception)
      {
         string key = null;
         string message = null;
         switch (exception.Number)
         {
            case 1048:
               key = GetValueRegex(exception.Message, @".*Column '(.*?)'.*");
               message = string.Format("This {0} is already taken", key);
               break;
            case 1062:
               key = GetValueRegex(exception.Message, @".*key '(.*?)'.*");
               message = string.Format("This {0} is already taken", key);
               break;
            default:
               key = string.Empty;
               message = exception.Message;
               break;
         }
         return new Tuple<string, string>(key, message);
      }

      private string GetValueRegex(string str, string regex)
      {
         return Regex.Match(str, regex).Groups[1].Value;
      }
   }
}
