using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using ShibaBot.Singletons;
using ShibaBot.Models;
using System.Threading;
using System;

namespace ShibaBot.Services {
    public class CommandHandler {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _provider;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider provider) {
            _client = client;
            _commands = commands;
            _provider = provider;

            _client.MessageReceived += MessageReceivedAsync ;
        }

        private Task MessageReceivedAsync(SocketMessage socketMessage) {
            new Thread(async () => {
                SocketUserMessage message = (SocketUserMessage)socketMessage;

                if (message == null ||
                    message.Author.Id == _client.CurrentUser.Id) return;

                SocketCommandContext context = new SocketCommandContext(_client, message);

                int argPos = 0;

                if (message.HasStringPrefix("sh!", ref argPos, StringComparison.OrdinalIgnoreCase) ||
                    message.HasStringPrefix("shiba ", ref argPos, StringComparison.OrdinalIgnoreCase) ||
                    message.HasMentionPrefix(_client.CurrentUser, ref argPos)) {
                    IResult result = await _commands.ExecuteAsync(context, argPos, _provider);

                    if (!result.IsSuccess) {
                        LocalesModel locales = await Language.GetLanguageAsync(context);

                        switch (result.Error) {
                            case CommandError.UnknownCommand:
                                await context.Channel.SendMessageAsync(locales.Errors.UnknownCommand);
                                break;
                            case CommandError.UnmetPrecondition:
                                switch (result.ErrorReason) {
                                    case "GuildOnly":
                                        await context.Channel.SendMessageAsync(locales.Errors.UnmetCondition.GuildOnly);
                                        break;
                                    case "UserManageGuild":
                                        await context.Channel.SendMessageAsync(locales.Errors.UnmetCondition.UserManageGuild);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case CommandError.BadArgCount:
                                await context.Channel.SendMessageAsync(locales.Errors.BadArgCount);
                                break;
                            case CommandError.ObjectNotFound:
                                await context.Channel.SendMessageAsync(locales.Errors.ObjectNotFound);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }).Start();
            return Task.CompletedTask;
        }
    }
}
