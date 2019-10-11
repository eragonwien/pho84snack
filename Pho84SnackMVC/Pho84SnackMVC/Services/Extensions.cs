using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

      public static string ReadString(this DbDataReader reader, string column, string defaultValue = "")
      {
         int ordinal = reader.GetOrdinal(column);
         if (!reader.IsDBNull(ordinal))
         {
            return reader.GetString(ordinal);
         }
         return defaultValue;
      }

      public static Int32 ReadInt32(this DbDataReader reader, string column, int defaultValue = 0)
      {
         int ordinal = reader.GetOrdinal(column);
         if (!reader.IsDBNull(ordinal))
         {
            return reader.GetInt32(ordinal);
         }
         return defaultValue;
      }

      public static decimal ReadDecimal(this DbDataReader reader, string column, decimal defaultValue = 0)
      {
         int ordinal = reader.GetOrdinal(column);
         if (!reader.IsDBNull(ordinal))
         {
            return reader.GetDecimal(ordinal);
         }
         return defaultValue;
      }

      public static async Task<int> ReadScalarInt32(this MySqlCommand cmd, int defaultValue = 0)
      {
         var result = await cmd.ExecuteScalarAsync();
         result = result != DBNull.Value ? result : null;
         return Convert.ToInt32(result);

      }
   }
}
