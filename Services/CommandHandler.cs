using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using ShibaBot.Singletons;
using System.Threading;
using System.Text.RegularExpressions;
using Discord;
using System;
using ShibaBot.Extensions;
using ShibaBot.Models;
using System.Linq;

namespace ShibaBot.Services {
    public class CommandHandler {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _provider;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider provider) {
            _client = client;
            _commands = commands;
            _provider = provider;

            _client.MessageReceived += MessageReceivedAsync;
        }

        private Task MessageReceivedAsync(SocketMessage socketMessage) {
            new Thread(async () =>
            {
                SocketUserMessage message = socketMessage as SocketUserMessage;

                if (message is null || message.Author.IsBot) return;

                CommandContext context = new CommandContext(_client, message);
                UtilitiesExtension utils = new UtilitiesExtension();

                int argPos = 0;

                string guildPrefix = await utils.GetPrefixAsync(context);

                if (message.HasStringPrefix(guildPrefix, ref argPos, StringComparison.OrdinalIgnoreCase) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) {
                    if (await utils.PermissionCheckAsync(context)) {
                        await _commands.ExecuteAsync(context, argPos, _provider);
                    }
                }
                else if (Regex.IsMatch(message.Content, $"^<@!?{_client.CurrentUser.Id}>$")) {
                    if (await utils.PermissionCheckAsync(context)) {
                        EmbedBuilder builder = new EmbedBuilder { Color = new Color(Utils.embedColor) };
                        LocalesModel locales = await Language.GetLanguageAsync(context);
                        builder.Title = locales.Mention.Replace("$prefix", guildPrefix);
                        await context.Channel.SendMessageAsync(embed: builder.Build());
                    }
                }

            }).Start();
            return Task.CompletedTask;
        }
    }
}
