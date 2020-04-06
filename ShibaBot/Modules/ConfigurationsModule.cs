using DSharpPlus.CommandsNext.Attributes;
using ConfigurationController.Models;
using MainDatabaseController.Models;
using ConfigurationController.DAO;
using MainDatabaseController.DAO;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using ShibaBot.Attributes;
using ShibaBot.Extensions;
using ShibaBot.Constants;
using DSharpPlus;
using System;

namespace ShibaBot.Modules {
    [Module("Configurations")]
    public class ConfigurationsModule {
        [RequireUserPermissions(Permissions.ManageGuild)]
        [Command("setlanguage"), Aliases("setlocale")]
        [Description("SetLocaleDescription")]
        public async Task SetLocaleAsync(CommandContext context, [Description("locale")] string locale) {
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder { Color = new DiscordColor(EmbedConstant.embedColor) };

            if (context.Channel.IsPrivate) {
                builder.Title = "This command can only be used in a guild.";
                await context.Channel.SendMessageAsync(embed: builder.Build());
                return;
            }

            GuildsDAO guildDAO = new GuildsDAO();

            StringsModel strings;

            StringsDAO stringsDAO = new StringsDAO();

            switch (locale.ToLower()) {
                case "pt-br":
                    strings = await stringsDAO.LoadAsync(new StringsModel { Identifier = "LocaleUpdated", Locale = 2 });
                    await guildDAO.SetAsync(new GuildsModel { ID = context.Guild.Id, Locale = 2 });
                    builder.Title = strings.String.Replace("$lang", "pt-BR");
                    break;
                case "en-us":
                    strings = await stringsDAO.LoadAsync(new StringsModel { Identifier = "LocaleUpdated", Locale = 1 });
                    await guildDAO.SetAsync(new GuildsModel { ID = context.Guild.Id, Locale = 1 });
                    builder.Title = strings.String.Replace("$lang", "en-US");
                    break;
                default:
                    int currentLocale = (int)(await new GuildsDAO().GetAsync(new GuildsModel { ID = context.Guild.Id })).Locale;
                    strings = await stringsDAO.LoadAsync(new StringsModel { Identifier = "InvalidLocale", Locale = currentLocale });
                    builder.Title = strings.String;
                    
                    //new CommandUseExtension().EmbedCommandUse(ref builder, locales, "locale", await guilds.GetPrefixAsync(Context.Guild.Id));
                    return;                    
            }
            await context.Channel.SendMessageAsync(embed: builder.Build());
        }
        [RequireUserPermissions(Permissions.ManageGuild)]
        [Command("setprefix")]
        [Description("SetPrefixDescription")]
        public async Task SetPrefixAsync(CommandContext context, [RemainingText] string prefix) {
            if (prefix == null)
                throw new ArgumentException();

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder() { Color = new DiscordColor(EmbedConstant.embedColor) };

            if (context.Channel.IsPrivate) {
                builder.Title = "This command can only be used in a guild.";
                await context.Channel.SendMessageAsync(embed: builder.Build());
                return;
            }

            StringsDAO stringsDAO = new StringsDAO();
            StringsModel strings = new StringsModel { Locale = (int)(await new GuildsDAO().GetAsync(new GuildsModel { ID = context.Guild.Id })).Locale };

            if (!prefix.Contains('`')) {
                if (prefix.Length <= 10) {
                    await new GuildsDAO().SetAsync(new GuildsModel { ID = context.Guild.Id, Prefix = prefix });
                    strings.Identifier = "PrefixUpdated";
                    builder.Title = (await stringsDAO.LoadAsync(strings)).String.Replace("$prefix", prefix);
                }
                else {
                    strings.Identifier = "InvalidPrefixSize";
                    builder.Title = (await stringsDAO.LoadAsync(strings)).String;
                    //new CommandUseExtension().EmbedCommandUse(ref builder, locales, "setprefix", await new GuildDAO().GetPrefixAsync(Context.Guild.Id));
                }
            }
            else {
                strings.Identifier = "InvalidPrefix";
                builder.Title = (await stringsDAO.LoadAsync(strings)).String;
                //new CommandUseExtension().EmbedCommandUse(ref builder, locales, "setprefix", await new GuildDAO().GetPrefixAsync(Context.Guild.Id));
            }

        await context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}