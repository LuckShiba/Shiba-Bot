using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
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
        }

        private async Task MessageReceivedAsync(SocketMessage socketMessage) {
            SocketUserMessage message = (SocketUserMessage) socketMessage;

            if (message == null ||
                message.Author.Id == _client.CurrentUser.Id) return;

            SocketCommandContext context = new SocketCommandContext(_client, message);

            int argPos = 0;

            if (message.HasStringPrefix("sh!", ref argPos, StringComparison.OrdinalIgnoreCase) ||
                message.HasStringPrefix("shiba ", ref argPos, StringComparison.OrdinalIgnoreCase) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) {

                IResult result = await _commands.ExecuteAsync(context, argPos, _provider);
            }
        }
    }
}
