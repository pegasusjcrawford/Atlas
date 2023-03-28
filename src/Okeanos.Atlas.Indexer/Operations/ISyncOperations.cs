using System.Threading.Tasks;
using Okeanos.Atlas.Indexer.Client;
using Okeanos.Atlas.Indexer.Client.Types;
using Okeanos.Atlas.Indexer.Operations.Types;

namespace Okeanos.Atlas.Indexer.Operations
{
   #region Using Directives

   #endregion Using Directives

   /// <summary>
   /// The SyncOperations interface.
   /// </summary>
   public interface ISyncOperations
   {
      /// <summary>
      /// The sync block.
      /// </summary>
      SyncPoolTransactions FindPoolTransactions(SyncConnection connection);

      /// <summary>
      /// The sync memory pool.
      /// </summary>
      SyncBlockTransactionsOperation SyncPool(SyncConnection connection, SyncPoolTransactions poolTransactions);

      /// <summary>
      /// The sync transactions.
      /// </summary>
      SyncBlockTransactionsOperation FetchFullBlock(SyncConnection connection, BlockInfo block);

      /// <summary>
      /// The check block reorganization.
      /// </summary>
      Task<Storage.Types.SyncBlockInfo> RewindToBestChain(SyncConnection connection);

      /// <summary>
      /// Delete all blocks that are not complete
      /// </summary>
      Task<Storage.Types.SyncBlockInfo> RewindToLastCompletedBlockAsync();

      /// <summary>
      /// Gets the height of the last block on the node.
      /// </summary>
      long GetBlockCount(IBlockchainClient client);

      void InitializeMmpool();
   }
}
