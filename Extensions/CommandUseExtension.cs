using Discord;
using Discord.Commands;
using ShibaBot.Models;

namespace ShibaBot.Extensions {
    public class CommandUseExtension {
        public void GetCommandUse(ref EmbedBuilder builder, LocalesModel locales, CommandInfo command) {
            LocalesModel.CommandsUseModel commandsUse = locales.CommandsUse;

            switch (command.Name) {
                case "shiba":
                    builder.AddField(new EmbedFieldBuilder() { IsInline = false, Name = commandsUse.CommandUse, Value = commandsUse.shiba });
                    break;
                case "shibabomb":
                    builder.AddField(new EmbedFieldBuilder() { IsInline = false, Name = commandsUse.CommandUse, Value = commandsUse.shibabomb });
                    break;
                case "avatar":
                    builder.AddField(new EmbedFieldBuilder() { IsInline = false, Name = commandsUse.CommandUse, Value = commandsUse.avatar[0] });
                    builder.AddField(new EmbedFieldBuilder() { IsInline = false, Name = commandsUse.Example, Value = commandsUse.avatar[1] });
                    break;
                case "locale":
                    builder.AddField(new EmbedFieldBuilder() { IsInline = false, Name = commandsUse.CommandUse, Value = commandsUse.locale[0] });
                    builder.AddField(new EmbedFieldBuilder() { IsInline = false, Name = commandsUse.Example, Value = commandsUse.locale[1] });
                    break;
                default:
                    break;
            }
        }
    }
}
