using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models
{
   public class Size
   {
      public long Id { get; set; }
      public string ShortName { get; set; }
      public string LongName { get; set; }
      public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

      public Size()
      {

      }

      public Size(long id = 0, string shortName = null, string longName = null)
      {
         Id = id;
         ShortName = shortName;
         LongName = longName;
      }
   }
}
