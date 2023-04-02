using MongoDB.Bson.Serialization.Attributes;

namespace Okeanos.Atlas.Indexer.Storage.Mongo.Types
{
  public class RichlistTable
   {
      [BsonId]
      public string Address { get; set; }
      public long Balance { get; set; }
   }
}
