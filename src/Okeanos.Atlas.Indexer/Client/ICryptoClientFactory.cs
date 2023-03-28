using Okeanos.Atlas.Indexer.Operations.Types;

namespace Okeanos.Atlas.Indexer.Client
{
   public interface ICryptoClientFactory
   {
      IBlockchainClient Create(SyncConnection connection);
   }
}
