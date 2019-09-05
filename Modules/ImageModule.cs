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
                Color = new Color(Utils.embedColor)
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
                Color = new Color(Utils.embedColor)
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
                Color = new Color(Utils.embedColor)
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
        [Command("reddit")]
        public async Task RedditAsync() {
            RedditModel.DataModel.ChildrenModel.DataModel redditJson;
            WebClient webClient = new WebClient();
            string jsonText = webClient.DownloadString("https://www.reddit.com/r/shiba/random.json?limit=1&obey_over18=true");
            redditJson = JsonConvert.DeserializeObject<List<RedditModel>>(jsonText)[0].Data.Children[0].Data;

            while (redditJson.IsVideo) {
                jsonText = webClient.DownloadString("https://www.reddit.com/r/shiba/random.json?limit=1");
                
                redditJson = JsonConvert.DeserializeObject<List<RedditModel>>(jsonText)[0].Data.Children[0].Data;
            }

            webClient.Dispose();
            EmbedBuilder builder = new EmbedBuilder {
                Color = new Color(Utils.embedColor),
                Description = WebUtility.HtmlDecode(redditJson.Title),
                Author = new EmbedAuthorBuilder { Name = $"/u/{redditJson.Author}" },
                Footer = new EmbedFooterBuilder { Text = redditJson.PermaLink },
                ImageUrl = redditJson.Url
            };

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
