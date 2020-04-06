using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using ShibaBot.Attributes;
using ShibaBot.Constants;

namespace ShibaBot.Modules {
    [Module("Utilities")]
    public class UtilitiesModule {
        [Command("avatar"), Aliases("pfp")]
        [Description("AvatarDescription")]
        public async Task AvatarAsync(CommandContext context, [RemainingText] DiscordUser user = null) {
            user ??= context.User;
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                ImageUrl = user.AvatarUrl,
                Title = $"{user.Username}#{user.Discriminator}",
                Color = new DiscordColor(EmbedConstant.embedColor)
            };
            await context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
