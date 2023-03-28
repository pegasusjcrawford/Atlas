using System.Collections.Generic;
using Okeanos.Atlas.Indexer.Operations.Types;
using Okeanos.Atlas.Indexer.Storage.Mongo.Types;

namespace Okeanos.Atlas.Indexer.Operations
{
   /// <summary>
   /// Maintain a cache of unspent outputs
   /// </summary>
   public interface IUtxoCache
   {
      int CacheSize { get; }

      UtxoCacheItem GetOne(string outpoint);
      void AddToCache(IEnumerable<OutputTable> outputs);

      void RemoveFromCache(IEnumerable<InputTable> inputs);
   }
}
