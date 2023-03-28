using System.Collections.Generic;
using Blockcore.Consensus.TransactionInfo;
using Okeanos.Atlas.Indexer.Client.Types;

namespace Okeanos.Atlas.Indexer.Operations.Types
{
   /// <summary>
   /// The sync block info.
   /// </summary>
   public class SyncBlockTransactionsOperation
   {
      /// <summary>
      /// Gets or sets the block info.
      /// </summary>
      public BlockInfo BlockInfo { get; set; }

      /// <summary>
      /// Gets or sets the transactions.
      /// </summary>
      public IEnumerable<Transaction> Transactions { get; set; }
   }
}
