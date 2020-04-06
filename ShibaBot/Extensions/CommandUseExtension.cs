using ConfigurationController.Models;
using ConfigurationController.DAO;
using System.Collections.Generic;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace ShibaBot.Extensions {
    internal class CommandUseExtension {
        internal async Task<string> GetCommandUseAsync(Command command, StringsModel strings, string prefix) {
            string argumentText = string.Empty;

            string commandUse = $"{prefix}{command.Name}";

            for (int i = 0; i < command.Arguments.Count; i++) {
                CommandArgument argument = command.Arguments[i];
                if (argument.Description != null) {
                    strings.Identifier = argument.Description + "Name";
                    string name = (await new StringsDAO().LoadAsync(strings)).String;

                    commandUse += $" [{name}]";

                    strings.Identifier = argument.Description + "Description";
                    string description = (await new StringsDAO().LoadAsync(strings)).String;
                    
                    argumentText += $"`{name}`: {description}\n";
                }
            }

            return $"`{commandUse}`\n\n{argumentText}";
        }
    }
}
