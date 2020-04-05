using System;
using System.Linq;
using System.Reflection;
using ShibaBot.Constants;
using ShibaBot.Extensions;
using ShibaBot.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using System.Collections.Generic;
using DSharpPlus.CommandsNext.Attributes;

namespace ShibaBot.Modules {
    public class HelpModule {
        [Command("help"), Aliases("ajuda", "commands", "comandos")]
        public async Task HelpAsync(CommandContext context, string commandName = null) {
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder { Color = new DiscordColor(EmbedConstant.embedColor) };

            if (commandName == null) {
                Type[] types = typeof(HelpModule).Assembly.GetTypes();
                for (int i = 0; i < types.Length; i++) {
                    ModuleAttribute moduleAttr = types[i].GetCustomAttribute<ModuleAttribute>();
                    if (moduleAttr != null) {
                        MethodInfo[] methods = types[i].GetMethods();
                        List<string> commands = new List<string>();
                        for (int j = 0; j < methods.Length; j++) {
                            CommandAttribute commandAttr = methods[j].GetCustomAttribute<CommandAttribute>();
                            if (commandAttr != null && methods[j].GetCustomAttribute<HiddenAttribute>() == null) 
                                commands.Add($"`{commandAttr.Name}`");
                        }
                        if (commands.Count != 0)
                            builder.AddField(moduleAttr.Name, string.Join(", ", commands));
                    }
                }
                await context.Channel.SendMessageAsync(embed: builder.Build());
            }
            else {
                Command command = context.CommandsNext.RegisteredCommands.FirstOrDefault(x => x.Key == commandName).Value;
                if (command == null) {
                    throw new ArgumentException();
                }

                builder.Title = $"**`{command.Name}`**: {command.Description}";
                await context.Channel.SendMessageAsync(embed: builder.Build());
            }
        }
    }
}