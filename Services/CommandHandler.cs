using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using ShibaBot.Singletons;
using System.Threading;
using System.Text.RegularExpressions;
using ShibaBot.Events;
using Discord;
using System;
using ShibaBot.Extensions;
using ShibaBot.Models;

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
            _commands.CommandExecuted += new CommandExecutedEvent().CommandExecutedAsync;
        }

        private Task MessageReceivedAsync(SocketMessage socketMessage) {
            new Thread(async () =>
            {
                SocketUserMessage message = socketMessage as SocketUserMessage;

                if (message == null || message.Author.Id == _client.CurrentUser.Id) return;

                CommandContext context = new CommandContext(_client, message);

                int argPos = 0;
                string guildPrefix = await new UtilitiesExtension().GetPrefixAsync(context);
                if (await new UtilitiesExtension().PermissionCheckAsync(context)) {
                    if (message.HasStringPrefix(guildPrefix, ref argPos, StringComparison.OrdinalIgnoreCase) ||
                    message.HasMentionPrefix(_client.CurrentUser, ref argPos)) {
                        await _commands.ExecuteAsync(context, argPos, _provider);
                    }
                    else if (Regex.IsMatch(message.Content, $"^<@!?{_client.CurrentUser.Id}>$")) {
                        EmbedBuilder builder = new EmbedBuilder { Color = new Color(Utils.embedColor) };
                        LocalesModel locales = await Language.GetLanguageAsync(context);
                        if (guildPrefix.EndsWith(' ')) { guildPrefix += "\u200b"; }
                        builder.Title = context.IsPrivate ? locales.MentionDM : locales.Mention.Replace("$prefix", guildPrefix);
                        await context.Channel.SendMessageAsync(embed: builder.Build());
                    }
                }
            }).Start();
            return Task.CompletedTask;
        }
    }
}
