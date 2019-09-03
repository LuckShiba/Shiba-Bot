using System;
using Discord;
using ShibaBot.Models;

namespace ShibaBot.Extensions {
    public class CommandUseExtension {
        public bool EmbedCommandUse(ref EmbedBuilder builder, LocalesModel locales, string commandName, string guildPrefix) {
            LocalesModel.CommandsUseModel commandsUse = locales.CommandsUse;

            foreach (LocalesModel.CommandsUseModel.CommandsModel command in commandsUse.Commands) {
                commandName = commandName.ToLower();
                if (command.Name == commandName || command.Aliases.Contains(commandName)) {
                    builder.Description = $"**{command.Name}:** ";
                    builder.Description += $"{command.Description}";
                    switch (command.Use) {
                        case 1:
                            AddFields(ref builder, guildPrefix,
                                NewField(commandsUse.CommandUse, command.Strings[0]));
                            break;
                        case 2:
                            AddFields(ref builder, guildPrefix,
                                NewField(commandsUse.CommandUse, command.Strings[0]),
                                NewField(commandsUse.Example, command.Strings[1]));
                            break;
                        case 3:
                            AddFields(ref builder, guildPrefix,
                                NewField(commandsUse.CommandUse, command.Strings[0]),
                                NewField(commandsUse.Examples, command.Strings[1]));
                            break;
                    }
                    return true;
                }
            }
            return false;
        }
        private Tuple<string, string> NewField(string name, string value) {
            return new Tuple<string, string>(name, value);
        }
        private void AddFields(ref EmbedBuilder builder, string guildPrefix, params Tuple<string, string>[] fields) {
            foreach (Tuple<string, string> field in fields) {
                builder.AddField(field.Item1, field.Item2.Replace("$prefix", guildPrefix));
            }
        }
    }
}
