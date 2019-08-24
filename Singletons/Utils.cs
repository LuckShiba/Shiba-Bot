using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using ShibaBot.Data.MySQL.DAO;

namespace ShibaBot.Singletons {
    public static class Utils {
        public static Color embedColor = new Color(0xef9e19);

        public static async Task<string> GetPrefixAsync(CommandContext context) {
            if (context.IsPrivate) {
                return await new GuildsDAO().GetPrefixAsync(context.Guild.Id);
            }
            return "shiba ";
        }
    }
}
