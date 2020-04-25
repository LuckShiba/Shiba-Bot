using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System.Linq;

namespace ShibaBot.Events {
    internal class MessageCreateEvent {
        private readonly DiscordClient _client;
        internal MessageCreateEvent (ref DiscordClient client) {
            _client = client;
            client.MessageCreated += MessageCreateAsync;
        }
        private async Task MessageCreateAsync(MessageCreateEventArgs eventArgs) {
            if (eventArgs.Message.Content.Length == 22 || eventArgs.Message.Content.Length == 21 && eventArgs.MentionedUsers.FirstOrDefault() != _client.CurrentUser) {
                // enviar a mensagem.
                return;
            }

            await _client.GetCommandsNext().HandleCommandsAsync(eventArgs);
        }
    }
}
