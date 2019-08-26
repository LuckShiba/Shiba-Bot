using Discord;
using Discord.Commands;
using ShibaBot.Models;
using System.Threading.Tasks;
using ShibaBot.Data.MySQL.DAO;

namespace ShibaBot.Singletons {
    public static class Utils {
        public static Color embedColor = new Color(0xef9e19);

        public static async Task<string> GetPrefixAsync(CommandContext context) {
            if (!context.IsPrivate) {
                return await new GuildsDAO().GetPrefixAsync(context.Guild.Id);
            }
            return "shiba ";
        }

        public static async Task<bool> PermissionCheckAsync(CommandContext context) {
            if (context.IsPrivate) return true;
            if (!(await context.Guild.GetCurrentUserAsync()).GetPermissions((IGuildChannel)context.Channel).SendMessages) {
                return false;
            }
            if (!(await context.Guild.GetCurrentUserAsync()).GetPermissions((IGuildChannel)context.Channel).EmbedLinks) {
                LocalesModel locales = await Language.GetLanguageAsync(context);
                await context.Channel.SendMessageAsync(locales.Errors.Forbidden.EmbedLinks);
                return false;
            }
            return true;
        }
    }
}
