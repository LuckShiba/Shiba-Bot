using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus;

namespace ShibaBot.Extensions {
    internal class PermissionExtension {
        internal async Task<bool> PermissionCheckAsync(CommandContext context) {
            if (context.Guild != null) {
                Permissions userPermissions = (await context.Guild.GetMemberAsync(context.Client.CurrentUser.Id)).PermissionsIn(context.Channel);

                if (!userPermissions.HasPermission(Permissions.EmbedLinks)) {
                    if (userPermissions.HasPermission(Permissions.SendMessages)) {
                        await context.Channel.SendMessageAsync("x"/*(await Language.GetLanguageAsync(context)).Errors.Forbidden.EmbedLinks*/);
                    }
                    return false;
                }
            }
            return true;
        }
    }
}
