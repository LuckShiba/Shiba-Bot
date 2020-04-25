using System;
using DSharpPlus;
using ShibaBot.Constants;
using ShibaBot.Extensions;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using ConfigurationController.DAO;
using MainDatabaseController.Models;
using ConfigurationController.Models;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using ConfigurationController.Enumerations;

namespace ShibaBot.Events {
    internal class CommandErroredEvent {
        internal async Task CommandErrored(CommandErrorEventArgs eventArgs) {
            switch (eventArgs.Exception) {
                case CommandNotFoundException _:
                    break;
                case ChecksFailedException e:
                    CheckBaseAttribute check = e.FailedChecks[0];
                    switch (check) {
                        case RequirePermissionsAttribute attr:
                            Console.WriteLine(attr.Permissions);
                            break;
                        case RequireUserPermissionsAttribute attr:
                            Console.WriteLine(attr.Permissions);
                            break;
                    }
                    break;
                case ArgumentException _:
                    GuildsModel guild = eventArgs.Context.Channel.IsPrivate ? null : new GuildsModel { ID = eventArgs.Context.Guild.Id };
                    Locale locale = new LocaleExtension().GetLocale(guild);
                    DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                        Color = new DiscordColor(ColorConstant.embedColor),
                        Title = (await new StringsDAO().LoadAsync(new StringsModel { 
                            Locale = locale,
                            Identifier = "ArgumentError"
                        })).String
                    };
                    await eventArgs.Context.Channel.SendMessageAsync(embed: await new CommandUseExtension().GetCommandUseAsync(builder, eventArgs.Context.Command, locale, new PrefixExtension().GetPrefix(guild)));
                    break;
                default:
                    eventArgs.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "Handler", eventArgs.Exception.StackTrace, DateTime.Now);
                    break;
            }
        }
    }
}
