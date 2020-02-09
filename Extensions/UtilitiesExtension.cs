using ShibaBot.Data.MySQL.DAO;
using ShibaBot.Singletons;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace ShibaBot.Extensions {
    public class UtilitiesExtension {
        public async Task<string> GetPrefixAsync(CommandContext context) {
            if (!context.IsPrivate) {
                return await new GuildDAO().GetPrefixAsync(context.Guild.Id);
            }
            return "s.";
        }

        public async Task<bool> PermissionCheckAsync(CommandContext context) {
            if (!context.IsPrivate) {
                ChannelPermissions userChannelPermissions = (await context.Guild.GetCurrentUserAsync()).GetPermissions(context.Channel as IGuildChannel);

                if (!userChannelPermissions.EmbedLinks) {
                    if (userChannelPermissions.SendMessages) {
                        await context.Channel.SendMessageAsync((await Language.GetLanguageAsync(context)).Errors.Forbidden.EmbedLinks);
                    }
                    return false;
                }
            }
            return true;
        }
    }
}
