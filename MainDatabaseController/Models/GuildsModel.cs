using MongoDB.Bson.Serialization.Attributes;

namespace MainDatabaseController.Models {
    public class GuildsModel {
        [BsonElement("_id")]
        public ulong ID { get; set; }
        [BsonElement("prefix")]
        [BsonIgnoreIfNull]
        public string Prefix { get; set; }
        [BsonElement("locale")]
        [BsonIgnoreIfNull]
        public int? Locale { get; set; }
    }
}
