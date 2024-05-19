using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver.Core.Misc;

namespace Course.Services.Catalog.Models
{
    public class Courses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }

        public Feature Feature { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        //kodlamada lazım olacağı için buaraya ekledik fakat BsonIgnore ile bunun mongodb de herhangibir karşılığı olmasın dedik
        [BsonIgnore]
        public Category Category { get; set; }
    }
}
