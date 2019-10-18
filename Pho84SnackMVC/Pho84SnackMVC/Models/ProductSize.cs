using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class ProductSize
   {
      public long Id { get; set; }
      public string ShortName { get; set; }
      public string LongName { get; set; }

      public ProductSize(long id, string shortName, string longName)
      {
         Id = id;
         ShortName = shortName;
         LongName = longName;
      }
   }
}
