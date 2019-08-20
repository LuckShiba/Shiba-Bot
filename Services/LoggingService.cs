using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ShibaBot.Models;
using ShibaBot.Singletons;
using System.Threading.Tasks;

namespace ShibaBot.Services {
    class LoggingService {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public LoggingService(DiscordSocketClient client, CommandService commands) {
            _client = client;
            _commands = commands;

            _client.Log += LogAsync;
            _commands.Log += LogAsync;
        }

        private async Task LogAsync (LogMessage log) {
            if (log.Exception is CommandException exception) {
                CommandContext context = (CommandContext)exception.Context;

                LocalesModel.ErrorsModel locales = (await Language.GetLanguageAsync(context)).Errors;
                if (!context.IsPrivate) {
                    if (!(await context.Guild.GetCurrentUserAsync()).GetPermissions((IGuildChannel)context.Channel).EmbedLinks && (await context.Guild.GetCurrentUserAsync()).GetPermissions((IGuildChannel)context.Channel).SendMessages) {
                        await context.Channel.SendMessageAsync(locales.Forbidden.EmbedLinks);
                    }
                }
            }

            else {
                string logMessage = $"{DateTime.Now.ToString("hh:mm:ss")} [{log.Severity}] {log.Source}: {log.Exception?.ToString() ?? log.Message}";

                Console.WriteLine(logMessage);
            }
        }
    }
}
