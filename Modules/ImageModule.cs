using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ShibaBot.Singletons;
using Newtonsoft.Json;

namespace ShibaBot.Modules {
    [Name("Image")]
    public class ImageModule : ModuleBase<CommandContext> {
        [Command("shiba", true), Alias("shibe")]
        public async Task ShibaAsync() {
            WebClient webClient = new WebClient();
            List<string> items = JsonConvert.DeserializeObject<List<string>>(webClient.DownloadString("https://shibe.online/api/shibes"));
            webClient.Dispose();

            EmbedBuilder builder = new EmbedBuilder {
                ImageUrl = items[0],
                Footer = new EmbedFooterBuilder {
                    Text = "shibe.online"
                },
                Color = new Color(Utils.embedColor)
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("bomb", true), Alias("shibabomb", "shibebomb", "shibasbomb", "shibesbomb")]
        public async Task ShibaBombAsync() {
            WebClient webClient = new WebClient();
            string jsonText = webClient.DownloadString("https://shibe.online/api/shibes?count=5");
            webClient.Dispose();

            await Context.Channel.SendMessageAsync(string.Join('\n', JsonConvert.DeserializeObject<List<string>>(jsonText)));
        }

        [Command("avatar"), Alias("pfp")]
        public async Task AvatarAsync([Remainder] SocketUser user = null) {
            user = user ?? Context.User as SocketUser;
            EmbedBuilder builder = new EmbedBuilder {
                ImageUrl = user.GetAvatarUrl(size: 1024) ?? user.GetDefaultAvatarUrl(),
                Title = user.ToString(),
                Color = new Color(Utils.embedColor)
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("husky", true)]
        public async Task HuskyAsync() {
            WebClient webClient = new WebClient();
            EmbedBuilder builder = new EmbedBuilder {
                ImageUrl = (string)JObject.Parse(webClient.DownloadString("https://dog.ceo/api/breed/husky/images/random"))["message"],
                Footer = new EmbedFooterBuilder {
                    Text = "dog.ceo"
                },
                Color = new Color(Utils.embedColor)
            };
            
            webClient.Dispose();
            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
