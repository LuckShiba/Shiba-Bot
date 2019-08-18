using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShibaBot.Modules {
    [Name("Image")]
    [RequireContext(ContextType.Guild, ErrorMessage = "GuildOnly")]
    public class ImageModule : ModuleBase<SocketCommandContext> {
        [Command("shiba"), Alias("shibe")]
        public async Task ShibaAsync() {
            string jsonText = new WebClient().DownloadString("https://shibe.online/api/shibes");
            List<string> items = JsonConvert.DeserializeObject<List<string>>(jsonText);

            EmbedBuilder builder = new EmbedBuilder() {
                ImageUrl = items[0],
                Footer = new EmbedFooterBuilder() {
                    Text = "shibe.online"
                },
                Color = new Color(0xef9e19)
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("avatar"), Alias("pfp")]
        public async Task AvatarAsync([Remainder] SocketUser user = null) {
            EmbedBuilder builder = new EmbedBuilder() {
                ImageUrl = (user ?? Context.User).GetAvatarUrl(size: 1024),
                Title = (user ?? Context.User).ToString(),
                Color = new Color(0xef9e19)
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
