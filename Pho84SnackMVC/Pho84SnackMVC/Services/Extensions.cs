using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public static class Extensions
   {
      public static string ReadString(this DbDataReader reader, string column, string defaultValue = "")
      {
         int ordinal = reader.GetOrdinal(column);
         if (!reader.IsDBNull(ordinal))
         {
            return reader.GetString(ordinal);
         }
         return defaultValue;
      }

      public static int ReadInt(this DbDataReader reader, string column, int defaultValue = 0)
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

      public static bool ReadBoolean(this DbDataReader reader, string column, bool defaultValue = false)
      {
         int ordinal = reader.GetOrdinal(column);
         if (!reader.IsDBNull(ordinal))
         {
            return reader.GetBoolean(ordinal);
         }
         return defaultValue;
      }

      public static bool IsDbNull(this DbDataReader reader, string column)
      {
         return reader.IsDBNull(reader.GetOrdinal(column));
      }

      public static async Task<int> ReadScalarInt32(this MySqlCommand cmd, int defaultValue = 0)
      {
         var result = await cmd.ExecuteScalarAsync();
         result = result != DBNull.Value ? result : null;
         return Convert.ToInt32(result);
      }


   }
}
