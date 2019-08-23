using Discord;
using Discord.Commands;
using ShibaBot.Models;

namespace ShibaBot.Extensions {
    public class CommandUseExtension {
        public void EmbedCommandUse(ref EmbedBuilder builder, LocalesModel locales, string commandName, string guildPrefix) {
            LocalesModel.CommandsUseModel commandsUse = locales.CommandsUse;

            switch (commandName) {
                case "shiba":
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.CommandUse, guildPrefix), Value = Format(commandsUse.shiba, guildPrefix)});
                    break;
                case "shibabomb":
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.CommandUse, guildPrefix), Value = Format(commandsUse.shibabomb, guildPrefix) });
                    break;
                case "avatar":
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.CommandUse, guildPrefix), Value = Format(commandsUse.avatar[0], guildPrefix) });
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.Example, guildPrefix), Value = Format(commandsUse.avatar[1], guildPrefix) });
                    break;
                case "locale":
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.CommandUse, guildPrefix), Value = Format(commandsUse.locale[0], guildPrefix) });
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.Example, guildPrefix), Value = Format(commandsUse.locale[1], guildPrefix) });
                    break;
                case "setprefix":
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.CommandUse, guildPrefix), Value = Format(commandsUse.setprefix[0], guildPrefix) });
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.Example, guildPrefix), Value = Format(commandsUse.setprefix[1], guildPrefix) });
                    builder.AddField(new EmbedFieldBuilder { IsInline = false, Name = Format(commandsUse.Example, guildPrefix), Value = Format(commandsUse.setprefix[2], guildPrefix) });
                    break;
            }
        }

        private static string Format(string original, string guildPrefix) {
            return original.Replace("$prefix", guildPrefix);
        }
    }
}
