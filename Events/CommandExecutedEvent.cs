using System.Threading.Tasks;
using Discord.Commands;
using ShibaBot.Singletons;
using ShibaBot.Models;
using ShibaBot.Extensions;
using Discord;

namespace ShibaBot.Events {
    public class CommandExecutedEvent {

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result) {
            if (!result.IsSuccess) {
                EmbedBuilder builder = new EmbedBuilder() { Color = Utils.embedColor };

                LocalesModel locales = await Language.GetLanguageAsync((CommandContext)context);

                switch (result.Error) {
                    case CommandError.UnmetPrecondition:
                        switch (result.ErrorReason) {
                            case "GuildOnly":
                                builder.Title = locales.Errors.UnmetCondition.GuildOnly;
                                await context.Channel.SendMessageAsync(embed: builder.Build());
                                break;
                            case "UserManageGuild":
                                builder.Title = locales.Errors.UnmetCondition.UserManageGuild;
                                await context.Channel.SendMessageAsync(embed: builder.Build());
                                break;
                            default:
                                break;
                        }
                        break;
                    case CommandError.BadArgCount:
                        builder.Title = locales.Errors.BadArgCount;
                        new CommandUseExtension().GetCommandUse(ref builder, locales, command.Value);
                        await context.Channel.SendMessageAsync(embed: builder.Build());

                        break;
                    case CommandError.ObjectNotFound:
                        builder.Title = locales.Errors.ObjectNotFound;
                        new CommandUseExtension().GetCommandUse(ref builder, locales, command.Value);
                        await context.Channel.SendMessageAsync(embed: builder.Build());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
