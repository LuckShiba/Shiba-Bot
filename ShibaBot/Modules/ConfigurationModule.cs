using DSharpPlus.CommandsNext.Attributes;
using MainDatabaseController.Models;
using MainDatabaseController.DAO;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using ShibaBot.Attributes;
using DSharpPlus.Entities;
using ShibaBot.Constants;
using System;

namespace ShibaBot.Modules {
    [Module("Configuration")]
    public class ConfigurationModule {
        [Command("setlocale"), Aliases("setlanguage")]
        public async Task SetLocaleAsync(CommandContext context, string locale) {
            GuildsDAO guildDAO = new GuildsDAO();

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder { Color = new DiscordColor(EmbedConstant.embedColor) };

            switch (locale.ToLower()) {
                case "pt-br":
                    await guildDAO.SetAsync(new GuildsModel { ID = context.Guild.Id, Locale = 2 });
                    //builder.Title = Language.pt_BR.Modules.Configuration.Locale.Replace("$lang", "pt-BR");
                    break;
                case "en-us":
                    await guildDAO.SetAsync(new GuildsModel { ID = context.Guild.Id, Locale = 1 });
                    //builder.Title = Language.en_US.Modules.Configuration.Locale.Replace("$lang", "en-US");
                    break;
                default:
                    //LocalesModel locales = await Language.GetLanguageAsync(Context);
                    //builder.Title = locales.Modules.Configuration.InvalidLocale;
                    //new CommandUseExtension().EmbedCommandUse(ref builder, locales, "locale", await guilds.GetPrefixAsync(Context.Guild.Id));
                    return;                    
            }
            await context.Channel.SendMessageAsync(embed: builder.Build());
        }
        [RequireUserPermissions(DSharpPlus.Permissions.Administrator)]
        [Command("setprefix")]
        public async Task SetPrefixAsync(CommandContext context, [RemainingText] string prefix) {
            //LocalesModel locales = await Language.GetLanguageAsync(Context);

            if (prefix == null)
                throw new ArgumentException("Not enough arguments supplied to the command.");

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder() { Color = new DiscordColor(EmbedConstant.embedColor) };
            if (!prefix.Contains('`')) {
                if (prefix.Length <= 10) {
                    await new GuildsDAO().SetAsync(new GuildsModel { ID = context.Guild.Id, Prefix = prefix });
                    //builder.Title = locales.Modules.Configuration.Prefix.Replace("$prefix", prefix);
                }
                else {
                    //builder.Title = locales.Modules.Configuration.InvalidPrefix[0];
                    //new CommandUseExtension().EmbedCommandUse(ref builder, locales, "setprefix", await new GuildDAO().GetPrefixAsync(Context.Guild.Id));
                }
            }
            else {
                //builder.Title = locales.Modules.Configuration.InvalidPrefix[1];
                //new CommandUseExtension().EmbedCommandUse(ref builder, locales, "setprefix", await new GuildDAO().GetPrefixAsync(Context.Guild.Id));
            }

        await context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}