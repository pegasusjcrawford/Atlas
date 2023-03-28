using Okeanos.Atlas.Indexer.Operations.Types;
using Okeanos.Atlas.Indexer.Storage.Types;

namespace Okeanos.Atlas.Indexer.Operations
{
   /// <summary>
   /// The StorageOperations interface.
   /// </summary>
   public interface IStorageOperations
   {

      void AddToStorageBatch(StorageBatch storageBatch, SyncBlockTransactionsOperation item);

      SyncBlockInfo PushStorageBatch(StorageBatch storageBatch);

      InsertStats InsertMempoolTransactions(SyncBlockTransactionsOperation item);
   }
}
