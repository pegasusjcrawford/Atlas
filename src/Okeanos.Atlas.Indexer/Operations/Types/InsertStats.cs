using System.Collections.Generic;
using Okeanos.Atlas.Indexer.Storage.Mongo.Types;

namespace Okeanos.Atlas.Indexer.Operations.Types
{
   #region Using Directives

   #endregion

   /// <summary>
   /// The insert stats.
   /// </summary>
   public class InsertStats
   {
      /// <summary>
      /// Gets or sets the transactions.
      /// </summary>
      public int Transactions { get; set; }

      public int RawTransactions { get; set; }

      /// <summary>
      /// Gets or sets the outputs.
      /// </summary>
      public int InputsOutputs { get; set; }

      /// <summary>
      /// Gets or sets the items.
      /// </summary>
      public List<MempoolTable> Items { get; set; }
   }
}
