using System;
using Discord;
using ShibaBot.Models;

namespace ShibaBot.Extensions {
    public class CommandUseExtension {
        public void EmbedCommandUse(ref EmbedBuilder builder, LocalesModel locales, string commandName, string guildPrefix) {
            LocalesModel.CommandsUseModel commandsUse = locales.CommandsUse;

            switch (commandName) {
                case "shiba":
                    AddFields(ref builder, guildPrefix,
                        NewField(commandsUse.CommandUse, commandsUse.shiba));
                    break;
                case "shibabomb":
                    AddFields(ref builder, guildPrefix,
                        NewField(commandsUse.CommandUse, commandsUse.shibabomb));
                    break;
                case "avatar":
                    AddFields(ref builder, guildPrefix,
                        NewField(commandsUse.CommandUse, commandsUse.avatar[0]),
                        NewField(commandsUse.Examples, commandsUse.avatar[1]));
                    break;
                case "husky":
                    AddFields(ref builder, guildPrefix,
                        NewField(commandsUse.CommandUse, commandsUse.husky));
                    break;
                case "locale":
                    AddFields(ref builder, guildPrefix,
                        NewField(commandsUse.CommandUse, commandsUse.locale[0]),
                        NewField(commandsUse.Example, commandsUse.locale[1]));
                    break;
                case "setprefix":
                    AddFields(ref builder, guildPrefix,
                        NewField(commandsUse.CommandUse, commandsUse.setprefix[0]),
                        NewField(commandsUse.Examples, commandsUse.setprefix[1]));
                    break;
            }
        }
        private Tuple<string, string> NewField(string name, string value) {
            return new Tuple<string, string>(name, value);
        }
        private void AddFields(ref EmbedBuilder builder, string guildPrefix, params Tuple<string, string>[] fields) {
            foreach(Tuple<string, string> field in fields) {
                builder.AddField(field.Item1, field.Item2.Replace("$prefix", guildPrefix));
            }
        }
    }
}
