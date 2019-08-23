using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using ShibaBot.Singletons;
using ShibaBot.Models;

namespace ShibaBot.Modules {
    [Name("Image")]
    public class ImageModule : ModuleBase<CommandContext> {
        [Command("shiba"), Alias("shibe")]
        public async Task ShibaAsync() {
            WebClient webClient = new WebClient();
            string jsonText = webClient.DownloadString("https://shibe.online/api/shibes");
            webClient.Dispose();
            List<string> items = JsonConvert.DeserializeObject<List<string>>(jsonText);

            EmbedBuilder builder = new EmbedBuilder {
                ImageUrl = items[0],
                Footer = new EmbedFooterBuilder {
                    Text = "shibe.online"
                },
                Color = Utils.embedColor
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("shibabomb"), Alias("shibebomb", "shibasbomb", "shibesbomb")]
        public async Task ShibaBombAsync() {
            WebClient webClient = new WebClient();
            string jsonText = webClient.DownloadString("https://shibe.online/api/shibes?count=5");
            webClient.Dispose();

            await Context.Channel.SendMessageAsync(string.Join('\n', JsonConvert.DeserializeObject<List<string>>(jsonText)));
        }

        [Command("avatar"), Alias("pfp")]
        public async Task AvatarAsync([Remainder] SocketUser user = null) {
            EmbedBuilder builder = new EmbedBuilder {
                ImageUrl = (user ?? Context.User).GetAvatarUrl(size: 1024),
                Title = (user ?? Context.User).ToString(),
                Color = Utils.embedColor
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("husky")]
        public async Task HuskyAsync() {
            WebClient webClient = new WebClient();
            string jsonText = webClient.DownloadString("https://dog.ceo/api/breed/husky/images/random");
            webClient.Dispose();
            string url = JsonConvert.DeserializeObject<DogCEOModel>(jsonText).message;

            EmbedBuilder builder = new EmbedBuilder {
                ImageUrl = url,
                Footer = new EmbedFooterBuilder {
                    Text = "dog.ceo"
                },
                Color = Utils.embedColor
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
