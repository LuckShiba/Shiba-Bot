using ConfigurationController.Enumerations;
using DSharpPlus.CommandsNext.Attributes;
using ConfigurationController.Models;
using MainDatabaseController.Models;
using ConfigurationController.DAO;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using ShibaBot.Extensions;
using ShibaBot.Attributes;
using ShibaBot.Constants;
using System.Reflection;
using System.Linq;
using System;
using MainDatabaseController.DAO;

namespace ShibaBot.Modules {
    public class HelpModule {
        [Command("help"), Aliases("ajuda", "commands", "comandos")]
        [Description("HelpDescription")]
        public async Task HelpAsync(CommandContext context, [Description("HelpCommandParameter")] string commandName = null) {
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder { 
                Color = new DiscordColor(ColorConstant.embedColor)
            };

            GuildsModel guild = context.Channel.IsPrivate ? null : await new GuildsDAO().GetAsync(new GuildsModel { ID = context.Guild.Id });
            Locale locale = new LocaleExtension().GetLocale(guild);

            if (commandName == null) {
                Type[] types = typeof(HelpModule).Assembly.GetTypes();
                for (int i = 0; i < types.Length; i++) {
                    ModuleAttribute moduleAttr = types[i].GetCustomAttribute<ModuleAttribute>();
                    if (moduleAttr != null) {
                        MethodInfo[] methods = types[i].GetMethods();
                        string commands = string.Empty;
                        for (int j = 0; j < methods.Length; j++) {
                            CommandAttribute commandAttr = methods[j].GetCustomAttribute<CommandAttribute>();
                            if (commandAttr != null && methods[j].GetCustomAttribute<HiddenAttribute>() == null) {
                                commands += $"`{commandAttr.Name}`, ";
                            }
                        }
                        if (commands.Length != 0) {
                            builder.AddField((await new StringsDAO().LoadAsync(new StringsModel {
                                Locale = locale,
                                Identifier = moduleAttr.Name
                            })).String, commands[..^2]);
                        }
                    }
                }
                await context.Channel.SendMessageAsync(embed: builder.Build());
            }
            else {
                Command command = context.CommandsNext.RegisteredCommands.FirstOrDefault(x => x.Key == commandName).Value;
                if (command == null)
                    throw new ArgumentException();
                builder.Title = $"`{command.Name}`: " +  (await new StringsDAO().LoadAsync(new StringsModel {
                    Locale = locale,
                    Identifier = command.Description
                })).String;
                if (command.Aliases.Count != 0) {
                    builder.Description = "\n" + (await new StringsDAO().LoadAsync(new StringsModel {
                        Locale = locale,
                        Identifier = "AKA"
                    })).String + ' ';
                    for (int i = 0; i < command.Aliases.Count; i++) 
                        builder.Description += $"`{command.Aliases[i]}`, ";
                    builder.Description = builder.Description[..^2];
                }
                builder = await new CommandUseExtension().GetCommandUseAsync(builder, context.Command, locale, new PrefixExtension().GetPrefix(guild));
                await context.Channel.SendMessageAsync(embed: builder.Build());
            }
        }
    }
}