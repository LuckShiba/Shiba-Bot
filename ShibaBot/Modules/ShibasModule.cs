using static DSharpPlus.Entities.DiscordEmbedBuilder;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DSharpPlus.Entities;
using ShibaBot.Extensions;
using ShibaBot.Attributes;
using ShibaBot.Constants;
using System.Net;

namespace ShibaBot.Modules {
    [Module("Shibas")]
    public class ShibasModule {
        [Command("shiba"), Aliases("shibe")]

        [Description("ShibaDescription")]
        public async Task ShibaAsync(CommandContext context) {
            WebClient webClient = new WebClient();
            string items = (string)JArray.Parse(await webClient.DownloadStringTaskAsync("https://shibe.online/api/shibes"))[0];
            webClient.Dispose();

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                ImageUrl = items,
                Footer = new EmbedFooter {
                    Text = "shibe.online"
                },
                Color = new DiscordColor(ColorConstant.embedColor)
            };

            await context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("bomb"), Aliases("shibabomb", "shibebomb", "shibasbomb", "shibesbomb")]
        [Description("BombDescription")]
        public async Task BombAsync(CommandContext context) {
            WebClient webClient = new WebClient();
            string jsonText = await webClient.DownloadStringTaskAsync("https://shibe.online/api/shibes?count=5");
            webClient.Dispose();

            await context.Channel.SendMessageAsync(string.Join('\n', JArray.Parse(jsonText)));
        }

        [Command("reddit"), Aliases("r/shiba")]
        [Description("RedditDescription")]
        public async Task RedditAsync(CommandContext context) {
            WebClient webClient = new WebClient();

            JToken data;
            do
                data = JArray.Parse(await webClient.DownloadStringTaskAsync("https://www.reddit.com/r/shiba/random/.json"))[0]["data"]["children"][0]["data"];
            while ((bool)data["is_video"] || !await new HttpsExtension().IsImageAsync((string)data["url"]));

            webClient.Dispose();

            string author = "u/" + (string)data["author"];

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                Author = new EmbedAuthor {
                    Name = author,
                    Url = $"https://www.reddit.com/{author}"
                },
                Color = new DiscordColor(ColorConstant.embedColor),
                ImageUrl = (string)data["url"],
                Footer = new EmbedFooter {
                    Text = "r/shiba",
                    IconUrl = "https://www.reddit.com/favicon.ico"
                }
            };

            string title = (string)data["title"];
            string url = $"https://www.reddit.com{data["permalink"]}";

            if (title.Length <= 256) {
                builder.Title = title;
                builder.Url = url;
            }
            else
                builder.Description = $"[{title}]({url})";

            await context.Channel.SendMessageAsync(embed: builder);
        }
    }
}
