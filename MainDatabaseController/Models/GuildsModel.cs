using MongoDB.Bson.Serialization.Attributes;
using ConfigurationController.Enumerations;

namespace MainDatabaseController.Models {
    public class GuildsModel {
        [BsonElement("_id")]
        public ulong ID { get; set; }
        [BsonElement("prefix")]
        [BsonIgnoreIfNull]
        public string Prefix { get; set; }
        [BsonElement("locale")]
        [BsonIgnoreIfNull]
        public Locale? Locale { get; set; }
    }
}
