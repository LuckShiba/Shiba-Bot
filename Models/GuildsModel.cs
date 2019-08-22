namespace ShibaBot.Models {
    public class GuildsModel {
        public ulong ID;
        public string Locale;
        public string Prefix;

        public GuildsModel(ulong ID, string Locale, string Prefix) {
            this.ID = ID;
            this.Locale = Locale;
            this.Prefix = Prefix;
        }
    }
}
