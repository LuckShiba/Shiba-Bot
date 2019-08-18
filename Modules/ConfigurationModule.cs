using System.Threading.Tasks;
using Discord.Commands;
using ShibaBot.Data.MySQL.DAO;
using ShibaBot.Singletons;
using ShibaBot.Models;

namespace ShibaBot.Modules {
    [Name("Configuration")]
    [RequireUserPermission(Discord.GuildPermission.ManageGuild, ErrorMessage="UserManageGuild")]
    public class ConfigurationModule: ModuleBase<SocketCommandContext> {
        [Command("locale"), Alias("language", "lang")]
        public async Task LocaleAsync(string locale) {
            GuildsDAO guilds = new GuildsDAO();
            LocalesModel.ModulesModel.ConfigurationModel language = (await Language.GetLanguageAsync(Context)).Modules.Configuration;
            switch (locale.ToLower()) {
                case "pt-br":
                    await guilds.UpdateLocale(Context.Guild.Id, "pt-BR");
                    await Context.Channel.SendMessageAsync(language.Locale.Replace("$lang", "pt-BR"));
                    break;
                case "en-us":
                    await guilds.UpdateLocale(Context.Guild.Id, "en-US");
                    await Context.Channel.SendMessageAsync(language.Locale.Replace("$lang", "en-US"));
                    break;
                default:
                    await Context.Channel.SendMessageAsync(language.InvalidLocale);
                    break;
            }
        }
    }
}
