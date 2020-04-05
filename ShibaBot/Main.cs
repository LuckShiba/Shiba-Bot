using MainDatabaseController.Singletons;
using ConfigurationController.Models;
using MainDatabaseController.Models;
using MainDatabaseController.DAO;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using ShibaBot.Events;
using DSharpPlus;
using System;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.CommandsNext.Converters;
using System.Collections.Generic;

namespace ShibaBot {
    public class Main {
        public readonly ShibaConfigModel _shibaConfig;
        public Main(ShibaConfigModel shibaConfig) {
            _shibaConfig = shibaConfig;
        }
        public async Task RunAsync() {
            DiscordClient client = new DiscordClient(new DiscordConfiguration {
                LogLevel = LogLevel.Info,                
                Token = _shibaConfig.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true
            });

            CommandsNextModule commandsNext = client.UseCommandsNext(new CommandsNextConfiguration {
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                CustomPrefixPredicate = GetPrefixPositionAsync
            });
            commandsNext.RegisterCommands(typeof(Main).Assembly);
            commandsNext.CommandErrored += new CommandErroredEvent().CommandErrored;

            DatabaseSingleton.Connect(_shibaConfig);
            await client.ConnectAsync();
            await Task.Delay(-1);
        }
        
        private async Task<int> GetPrefixPositionAsync(DiscordMessage message) {
            string prefix = null;
            if (message.Channel.Guild != null) {
                prefix = (await new GuildsDAO().GetAsync(new GuildsModel { ID = message.Channel.Guild.Id })).Prefix;
            }
            prefix ??= "s.";

            return message.Content.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ? prefix.Length : -1;
        }
    }
}
