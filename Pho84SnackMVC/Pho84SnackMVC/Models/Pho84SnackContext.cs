using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class Pho84SnackContext
   {
      public string ConnectionString { get; set; }

      public Pho84SnackContext(string connectionString)
      {
         ConnectionString = connectionString;
      }

      public MySqlConnection GetConnection()
      {
         MySqlConnection connection = new MySqlConnection(ConnectionString);
         if (connection != null && connection.State == System.Data.ConnectionState.Closed)
         {
            connection.Open();
         }
         return connection;
      }
   }
}
