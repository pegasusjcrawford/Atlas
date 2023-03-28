using System.Collections.Generic;
using Okeanos.Atlas.Indexer.Storage.Mongo.Types;

namespace Okeanos.Atlas.Indexer.Operations.Types
{
   #region Using Directives

   #endregion Using Directives

   public class StorageBatch
   {
      public long TotalSize { get; set; }
      public List<TransactionBlockTable> TransactionBlockTable { get; set; } = new();
      public Dictionary<long, BlockTable> BlockTable { get; set; } = new();
      public List<TransactionTable> TransactionTable { get; set; } = new();
      public Dictionary<string,OutputTable> OutputTable { get; set; } = new();
      public List<InputTable> InputTable { get; set; } = new();

      public object ExtraData { get; set; }
   }
}
