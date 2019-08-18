namespace ShibaBot.Models {
    public class GuildsModel {
        public ulong ID;
        public string Locale;

        public GuildsModel(ulong ID, string Locale) {
            this.ID = ID;
            this.Locale = Locale;
        }
    }
}
