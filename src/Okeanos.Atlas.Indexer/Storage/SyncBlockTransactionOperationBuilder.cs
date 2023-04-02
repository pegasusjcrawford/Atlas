using Okeanos.Atlas.Indexer.Client.Types;
using Okeanos.Atlas.Indexer.Operations.Types;
using NBitcoin;

namespace Okeanos.Atlas.Indexer.Storage
{
   public class SyncBlockTransactionOperationBuilder : ISyncBlockTransactionOperationBuilder
   {

      public SyncBlockTransactionsOperation BuildFromClientData(BlockInfo blockInfo, Block block)
      {
         return new SyncBlockTransactionsOperation { BlockInfo = blockInfo, Transactions = block.Transactions };
      }
   }
}
