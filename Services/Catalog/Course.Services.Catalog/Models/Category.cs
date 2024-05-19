using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Course.Services.Catalog.Models
{
    public class Category
    {
        [BsonId] //mongodb tarafından id olarak algılanması için
        [BsonRepresentation(BsonType.ObjectId)] //id'nin mongo db tarafından tipini belirtmek lazım
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
