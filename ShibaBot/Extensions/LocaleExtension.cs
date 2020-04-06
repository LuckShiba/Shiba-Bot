using DSharpPlus.Entities;
using System.Threading.Tasks;
using MainDatabaseController.DAO;
using MainDatabaseController.Models;

namespace ShibaBot.Extensions {
    internal class LocaleExtension {
        internal async Task<int> GetLocaleAsync(DiscordGuild guild) {
            int? locale = null;
            if (guild != null) {
                locale = (await new GuildsDAO().GetAsync(new GuildsModel { ID = guild.Id })).Locale;
            }
            locale ??= 1;

            return (int)locale;
        }
    }
}
