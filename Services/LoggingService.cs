using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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

        private Task LogAsync (LogMessage log) {
            string logMessage = $"{DateTime.Now.ToString("hh:mm:ss")} [{log.Severity}] {log.Source}: {log.Exception?.ToString() ?? log.Message}";

            return Console.Out.WriteLineAsync(logMessage);
        }
    }
}
