using System.Threading.Tasks;
using Discord.Commands;
using ShibaBot.Data.MySQL.DAO;
using ShibaBot.Extensions;
using ShibaBot.Singletons;
using ShibaBot.Models;
using Discord;

namespace ShibaBot.Modules {
    [Name("Configuration")]
    [RequireContext(ContextType.Guild, ErrorMessage = "GuildOnly")]
    [RequireUserPermission(GuildPermission.ManageGuild, ErrorMessage = "UserManageGuild")]
    public class ConfigurationModule : ModuleBase<CommandContext> {
        [Command("locale"), Alias("language", "lang")]
        public async Task LocaleAsync(string locale) {
            GuildsDAO guilds = new GuildsDAO();

            EmbedBuilder builder = new EmbedBuilder() { Color = Utils.embedColor };

            switch (locale.ToLower()) {
                case "pt-br":
                    await guilds.UpdateLocaleAsync(Context.Guild.Id, "pt-BR");
                    LocalesModel language_pt_br = await Language.GetLanguageAsync(Context);
                    builder.Title = language_pt_br.Modules.Configuration.Locale.Replace("$lang", "pt-BR");
                    break;
                case "en-us":
                    await guilds.UpdateLocaleAsync(Context.Guild.Id, "en-US");
                    LocalesModel language_en_us = await Language.GetLanguageAsync(Context);
                    builder.Title = language_en_us.Modules.Configuration.Locale.Replace("$lang", "en-US");
                    break;
                default:
                    LocalesModel locales = await Language.GetLanguageAsync(Context);
                    builder.Title = locales.Modules.Configuration.InvalidLocale;
                    new CommandUseExtension().EmbedCommandUse(ref builder, locales, "locale", await guilds.GetPrefixAsync(Context.Guild.Id));
                    break;
            }

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("setprefix"), Alias("prefix", "prefixo")]
        public async Task SetPrefixAsync([Remainder] string prefix) {
            LocalesModel locales = await Language.GetLanguageAsync(Context);
            EmbedBuilder builder = new EmbedBuilder() { Color = Utils.embedColor };
            if (!prefix.Contains('`')) {
                string[] prefixs = prefix.Split(' ');
                if (prefixs.Length > 1 && prefixs[prefixs.Length - 1] == "cmd") {
                    prefix = "";
                    for (int i = 0; i <= prefixs.Length - 2; i++) {
                        prefix += $"{prefixs[i]} ";
                    }
                }

                if (prefix.Length <= 10) {
                    await new GuildsDAO().UpdatePrefixAsync(Context.Guild.Id, prefix);
                    if (prefix.EndsWith(' ')) { prefix += "\u200b"; }
                    builder.Title = locales.Modules.Configuration.Prefix.Replace("$prefix", prefix);
                }
                else {
                    builder.Title = locales.Modules.Configuration.InvalidPrefix[0];
                    new CommandUseExtension().EmbedCommandUse(ref builder, locales, "setprefix", await new GuildsDAO().GetPrefixAsync(Context.Guild.Id));
                }
            }
            else {
                builder.Title = locales.Modules.Configuration.InvalidPrefix[1];
                new CommandUseExtension().EmbedCommandUse(ref builder, locales, "setprefix", await new GuildsDAO().GetPrefixAsync(Context.Guild.Id));
            }

        await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}