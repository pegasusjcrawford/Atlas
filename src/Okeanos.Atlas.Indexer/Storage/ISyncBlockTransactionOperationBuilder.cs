using Okeanos.Atlas.Indexer.Client.Types;
using Okeanos.Atlas.Indexer.Operations.Types;
using NBitcoin;

namespace Okeanos.Atlas.Indexer.Storage
{
   public interface ISyncBlockTransactionOperationBuilder
   {
      SyncBlockTransactionsOperation BuildFromClientData(BlockInfo blockInfo, Block block);
   }
}
