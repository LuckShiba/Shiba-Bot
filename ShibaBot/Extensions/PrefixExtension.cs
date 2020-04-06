using DSharpPlus.Entities;
using System.Threading.Tasks;
using MainDatabaseController.DAO;
using MainDatabaseController.Models;

namespace ShibaBot.Extensions {
    internal class PrefixExtension {
        internal async Task<string> GetPrefixAsync(DiscordGuild guild) {
            string prefix = null;
            if (guild != null) {
                prefix = (await new GuildsDAO().GetAsync(new GuildsModel { ID = guild.Id })).Prefix;
            }
            prefix ??= "s.";

            return prefix;
        }
    }
}
