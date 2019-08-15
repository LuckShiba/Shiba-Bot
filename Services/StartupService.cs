using System.Threading.Tasks;
using System.Reflection;
using ShibaBot.Data.DAO;
using Discord.WebSocket;
using Discord.Commands;
using Discord;
using System;

namespace ShibaBot.Services {
    public class StartupService {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public StartupService(IServiceProvider provider, DiscordSocketClient client, CommandService commands) {
            _provider = provider;
            _client = client;
            _commands = commands;
        }
        public async Task RunAsync() {
            await _client.LoginAsync(TokenType.Bot, new ShibaConfigDAO().Load().Token);
            await _client.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}
