using System.Collections.Generic;

namespace Okeanos.Atlas.Indexer.Storage.Types
{
   public class QueryResult<T>
   {
      public int Offset { get; set; }

      public int Limit { get; set; }

      public long Total { get; set; }

      public IEnumerable<T> Items { get; set; }
   }
}
