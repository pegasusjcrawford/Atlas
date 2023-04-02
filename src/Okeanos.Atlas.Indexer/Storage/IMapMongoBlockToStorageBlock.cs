using Okeanos.Atlas.Indexer.Client.Types;
using Okeanos.Atlas.Indexer.Storage.Mongo.Types;
using Okeanos.Atlas.Indexer.Storage.Types;

namespace Okeanos.Atlas.Indexer.Storage
{
   public interface IMapMongoBlockToStorageBlock
   {
      SyncBlockInfo Map(BlockTable block);
      BlockTable Map(BlockInfo blockInfo);
   }
}
