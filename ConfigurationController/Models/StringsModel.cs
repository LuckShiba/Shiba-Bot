using ConfigurationController.Enumerations;

namespace ConfigurationController.Models {
    public class StringsModel {
        public uint ID { get; set; }
        public string Identifier { get; set; }
        public string String { get; set; }
        public Locale Locale { get; set; }
    }
}
