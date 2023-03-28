using System.Collections.Generic;
using System.Threading.Tasks;
using Okeanos.Atlas.Indexer.Client.Types;

namespace Okeanos.Atlas.Indexer.Client
{
   public interface IBlockchainClient
   {
      Task<string> SentRawTransactionAsync(string transactionHex);
      Task<int> GetConnectionCountAsync();
      Task<string> GetblockHashAsync(long blockIndex);
      Task<BlockInfo> GetBlockAsync(string blockHash);
      IEnumerable<string> GetRawMemPool();
      string GetBlockHex(string blockHash);
      DecodedRawTransaction GetRawTransaction(string itemItem, int verbose);
      Task<BlockchainInfoModel> GetBlockchainInfo();
      Task<NetworkInfoModel> GetNetworkInfo();
      Task<IEnumerable<PeerInfo>> GetPeerInfo();
      Task<DecodedRawTransaction> GetRawTransactionAsync(string transactionId, int verbose);
      int GetBlockCount();
   }
}
