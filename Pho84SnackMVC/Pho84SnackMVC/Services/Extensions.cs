using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace Pho84SnackMVC.Services
{
   public static class Extensions
   {
      public static string ReadString(this MySqlDataReader reader, string column, string defaultValue = "")
      {
         int ordinal = reader.GetOrdinal(column);
         if (!reader.IsDBNull(ordinal))
         {
            return reader.GetString(ordinal);
         }
         return defaultValue;
      }

      public static int ReadScalarInt32(this MySqlCommand cmd, int defaultValue = 0)
      {
         var result = cmd.ExecuteScalar();
         result = result != DBNull.Value ? result : null;
         return Convert.ToInt32(result);

      }
   }
}
