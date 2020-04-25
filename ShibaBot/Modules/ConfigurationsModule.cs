using ConfigurationController.Enumerations;
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
        public async Task SetLocaleAsync(CommandContext context, [Description("Locale")] string locale) {
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                Color = new DiscordColor(ColorConstant.embedColor)
            };

            if (context.Channel.IsPrivate) {
                builder.Title = "This command can only be used in a guild.";
                await context.Channel.SendMessageAsync(embed: builder.Build());
                return;
            }

            GuildsModel guild = await new GuildsDAO().GetAsync(new GuildsModel { ID = context.Guild.Id });

            switch (locale.ToLower()) {
                case "pt-br":
                    guild.Locale = Locale.PT_BR;
                    await new GuildsDAO().SetAsync(guild);

                    builder.Title = (await new StringsDAO().LoadAsync(new StringsModel {
                        Identifier = "LocaleUpdated",
                        Locale = Locale.PT_BR,
                    })).String.Replace("$lang", "pt-BR");
                    break;
                case "en-us":
                    guild.Locale = Locale.EN_US;
                    await new GuildsDAO().SetAsync(guild);
                    builder.Title = (await new StringsDAO().LoadAsync(new StringsModel {
                        Identifier = "LocaleUpdated",
                        Locale = Locale.EN_US,
                    })).String.Replace("$lang", "en-US");
                    break;
                default:
                    builder.Title = (await new StringsDAO().LoadAsync(new StringsModel {
                        Identifier = "InvalidLocale",
                        Locale = new LocaleExtension().GetLocale(guild)
                    })).String;

                    throw new ArgumentException();
            }
            await context.Channel.SendMessageAsync(embed: builder.Build());
        }
        [RequireUserPermissions(Permissions.ManageGuild)]
        [Command("setprefix")]
        [Description("SetPrefixDescription")]
        public async Task SetPrefixAsync(CommandContext context, [RemainingText] string prefix) {
            if (prefix == null)
                throw new ArgumentException();

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder() {
                Color = new DiscordColor(ColorConstant.embedColor)
            };

            if (context.Channel.IsPrivate) {
                builder.Title = "This command can only be used in a guild.";
                await context.Channel.SendMessageAsync(embed: builder.Build());
                return;
            }

            GuildsModel guild = await new GuildsDAO().GetAsync(new GuildsModel { ID = context.Guild.Id });
            
            Locale locale = new LocaleExtension().GetLocale(guild);

            if (!prefix.Contains('`')) {
                if (prefix.Length <= 10) {
                    await new GuildsDAO().SetAsync(new GuildsModel {
                        ID = context.Guild.Id,
                        Prefix = prefix
                    });
                    builder.Title = (await new StringsDAO().LoadAsync(new StringsModel {
                        Locale = locale,
                        Identifier = "PrefixUpdated".Replace("$prefix", prefix)
                    })).String.Replace("$prefix", prefix);
                }
                else {
                    builder.Title = (await new StringsDAO().LoadAsync(new StringsModel {
                        Locale = locale,
                        Identifier = "InvalidPrefixSize"
                    })).String;
                    await context.Channel.SendMessageAsync(embed: await new CommandUseExtension().GetCommandUseAsync(builder, context.Command, locale, new PrefixExtension().GetPrefix(guild)));
                }
            }
            else {
                builder.Title = (await new StringsDAO().LoadAsync(new StringsModel {
                    Locale = locale,
                    Identifier = "InvalidPrefix"
                })).String;
            }

            await context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}