using System.Threading.Tasks;

namespace Okeanos.Atlas.Indexer.Storage.Mongo;

public interface IBlockRewindOperation
{
   Task RewindBlockAsync(uint blockIndex);
}
