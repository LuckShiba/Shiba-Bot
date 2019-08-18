using Discord.Commands;

namespace ShibaBot.Modules {
    [RequireUserPermission(Discord.GuildPermission.ManageGuild, ErrorMessage="UserManageGuild")]
    public class ConfigurationModule: ModuleBase<SocketCommandContext> {
    }
}
