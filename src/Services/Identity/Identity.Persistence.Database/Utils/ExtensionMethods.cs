using MongoDB.Bson;
using MongoDB.Driver;

namespace Identity.Persistence.Database.Utils
{
    public static class ExtensionMethods
    {
        public static bool CollectionExists<T>(this IMongoCollection<T> collection)
        {
            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter);
            return (totalCount > 0) ? true : false;
        }
    }
}
