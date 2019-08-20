using System.Threading.Tasks;
using Discord.Commands;
using ShibaBot.Data.MySQL.DAO;
using ShibaBot.Singletons;
using ShibaBot.Models;
using Discord;

namespace ShibaBot.Modules {
    [Name("Configuration")]
    [RequireUserPermission(Discord.GuildPermission.ManageGuild, ErrorMessage="UserManageGuild")]
    public class ConfigurationModule: ModuleBase<CommandContext> {
        [Command("locale"), Alias("language", "lang")]
        public async Task LocaleAsync(string locale) {
            GuildsDAO guilds = new GuildsDAO();
            LocalesModel.ModulesModel.ConfigurationModel language = (await Language.GetLanguageAsync(Context)).Modules.Configuration;

            EmbedBuilder builder = new EmbedBuilder() { Color = Utils.embedColor };
            switch (locale.ToLower()) {
                case "pt-br":
                    await guilds.UpdateLocale(Context.Guild.Id, "pt-BR");
                    builder.Title = language.Locale.Replace("$lang", "pt-BR");
                    await Context.Channel.SendMessageAsync(embed: builder.Build());
                    break;
                case "en-us":
                    await guilds.UpdateLocale(Context.Guild.Id, "en-US");
                    builder.Title = language.Locale.Replace("$lang", "en-US");
                    await Context.Channel.SendMessageAsync(embed: builder.Build());
                    break;
                default:
                    builder.Title = language.InvalidLocale;
                    await Context.Channel.SendMessageAsync(embed: builder.Build());
                    break;
            }
        }
    }
}
