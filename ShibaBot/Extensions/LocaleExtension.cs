using MainDatabaseController.DAO;
using MainDatabaseController.Models;
using ConfigurationController.Enumerations;

namespace ShibaBot.Extensions {
    internal class LocaleExtension {
        internal Locale GetLocale(GuildsModel guild) {
            return guild?.Locale ?? Locale.EN_US;
        }
    }
}
