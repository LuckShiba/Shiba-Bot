using ConfigurationController.Enumerations;
using ConfigurationController.Models;
using ConfigurationController.DAO;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace ShibaBot.Extensions {
    internal class CommandUseExtension {
        internal async Task<DiscordEmbedBuilder> GetCommandUseAsync(DiscordEmbedBuilder embed, Command command, Locale locale, string prefix) {
            string argumentText = string.Empty;

            string commandUse = $"{prefix}{command.Name}";

            foreach (CommandArgument argument in command.Arguments) {
                if (argument.Description != null) {
                    string name = (await new StringsDAO().LoadAsync(new StringsModel {
                        Locale = locale,
                        Identifier = argument.Description + "Name"
                    })).String;

                    commandUse += $" [{name}]";

                    string description = (await new StringsDAO().LoadAsync(new StringsModel {
                        Locale = locale,
                        Identifier = argument.Description + "Description"
                    })).String;

                    argumentText += $"`{name}`: {description}\n";
                }
            }

            embed.AddField((await new StringsDAO().LoadAsync(new StringsModel {
                Locale = locale,
                Identifier = "CommandUse"
            })).String, $"`{commandUse}`\n\n{argumentText}");
            return embed;
        }
    }
}
