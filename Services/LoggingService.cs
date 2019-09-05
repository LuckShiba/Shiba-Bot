using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ShibaBot.Extensions;
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

        private async Task LogAsync(LogMessage log) {
            if (log.Exception is CommandException exception) {
                await new UtilitiesExtension().PermissionCheckAsync(exception.Context as CommandContext);
            }

            else {
                Log(log);
            }
        }
        private void Log(LogMessage log) {
            if (log.Severity == LogSeverity.Warning || log.Severity == LogSeverity.Error || log.Severity == LogSeverity.Critical) {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            else if (log.Severity == LogSeverity.Info) {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            string logMessage = $"{DateTime.Now.ToString("hh:mm:ss")} [{log.Severity}] {log.Source}: {log.Exception?.ToString() ?? log.Message}";

            Console.WriteLine(logMessage);

            Console.ResetColor();
        }

    }
}
