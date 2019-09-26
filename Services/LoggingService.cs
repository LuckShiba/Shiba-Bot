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

        private async Task LogAsync(LogMessage logMessage) {
            if (logMessage.Exception is CommandException exception) {
                if (!await new UtilitiesExtension().PermissionCheckAsync(exception.Context as CommandContext)) {
                    Log(logMessage);
                }
            }

            else {
                Log(logMessage);
            }
        }
        private void Log(LogMessage logMessage) {
            if (logMessage.Severity == LogSeverity.Warning || logMessage.Severity == LogSeverity.Error || logMessage.Severity == LogSeverity.Critical) {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            else if (logMessage.Severity == LogSeverity.Info) {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            string logString = $"{DateTime.Now.ToString("hh:mm:ss")} [{logMessage.Severity}] {logMessage.Source}: {logMessage.Exception?.ToString() ?? logMessage.Message}";

            Console.WriteLine(logString);

            Console.ResetColor();
        }

    }
}
