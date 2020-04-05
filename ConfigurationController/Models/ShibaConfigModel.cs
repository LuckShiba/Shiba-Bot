namespace ConfigurationController.Models {
    public class ShibaConfigModel {
        public ulong OwnerID { get; set; }
        public string Token { get; set; }
        public string MongoConnectionString { get; set; }
        public ShibaConfigModel(ulong ownerID, string token, string mongoConnectionString) {
            OwnerID = ownerID;
            Token = token;
            MongoConnectionString = mongoConnectionString;
        }
    }
}