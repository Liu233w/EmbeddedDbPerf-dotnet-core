using System.Collections.Generic;
using System.Linq;
using LiteDB;
using TestPerfLiteDB.Entities;

namespace TestPerfLiteDB
{
    public static class DocExtensions
    {
        public static IEnumerable<BsonDocument> ToBson(this IEnumerable<Doc> docs)
        {
            return docs.Select(doc => new BsonDocument
            {
                {"_id", doc.Id},
                {"name", doc.Name},
                {"lorem", doc.Lorem}
            });
        }
    }
}