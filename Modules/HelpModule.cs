using Discord;
using ShibaBot.Models;
using Discord.Commands;
using ShibaBot.Extensions;
using ShibaBot.Singletons;
using System.Threading.Tasks;

namespace ShibaBot.Modules {
    public class HelpModule : ModuleBase<CommandContext> {
        [Command("help"), Alias("ajuda", "commands", "comandos")]
        public async Task HelpCommand(string module = null) {
            LocalesModel lang = await Language.GetLanguageAsync(Context);
            EmbedBuilder builder = new EmbedBuilder { Color = Utils.embedColor };
            if (module is null) {
                string fieldValue = "";
                builder.Description = lang.Modules.Help.Help.Replace("$prefix", await Utils.GetPrefixAsync(Context));
                foreach (LocalesModel.ModulesModel.HelpModel.ModuleModel helpModule in lang.Modules.Help.Modules) {
                    fieldValue += $"{helpModule.Emoji} {helpModule.Name}\n";
                }
                builder.AddField(lang.Modules.Help.ModulesEmbed, fieldValue);
            }
            else {
                bool match = false;
                foreach (LocalesModel.ModulesModel.HelpModel.ModuleModel helpModule in lang.Modules.Help.Modules) {
                    if (module.ToLower() == helpModule.Name.ToLower() ) {
                        match = true;
                        builder.Title = $"{helpModule.Emoji} {helpModule.Name}";
                        builder.Description = helpModule.Description;
                        builder.AddField(lang.Modules.Help.Commands, string.Join('\n', helpModule.Commands));
                        break;
                    }
                }
                if (!match) {
                    new CommandUseExtension().EmbedCommandUse(ref builder, lang, "help", await Utils.GetPrefixAsync(Context));
                }
            }
            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
