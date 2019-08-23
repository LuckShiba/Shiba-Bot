using Discord.Commands;
using System.Threading.Tasks;

namespace ShibaBot.Modules {
    public class HelpModule : ModuleBase<CommandContext> {
        [Command("help"), Alias("ajuda", "commands", "comandos")]
        public async Task HelpCommand(string module = null) {
            
        }
    }
}
