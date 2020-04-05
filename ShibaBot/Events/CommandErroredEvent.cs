using System;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;

namespace ShibaBot.Events {
    internal class CommandErroredEvent {
        internal async Task CommandErrored(CommandErrorEventArgs eventArgs) {
            switch (eventArgs.Exception) {
                case CommandNotFoundException _:
                    // comando nao encontrado yay
                    //mensagem de comando não encontrado?
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
                case ArgumentException e:
                    Console.WriteLine(e);
                    // enviar o command use
                    break;
                default:
                    eventArgs.Context?.Client.DebugLogger.LogMessage(LogLevel.Error, "Handler", eventArgs.Exception.Message, DateTime.Now);
                    break;
            }
        }
    }
}
