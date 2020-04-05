using static DSharpPlus.Entities.DiscordEmbedBuilder;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ShibaBot.Attributes;
using DSharpPlus.Entities;
using ShibaBot.Constants;
using System.Net;

namespace ShibaBot.Modules {
    [Module("Image")]
    public class ImageModule  {
        [Command("shiba"), Aliases("shibe")]
        public async Task ShibaAsync(CommandContext context) {
            WebClient webClient = new WebClient();
            string items = (string)JArray.Parse(webClient.DownloadString("https://shibe.online/api/shibes"))[0];
            webClient.Dispose();

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                ImageUrl = items,
                Footer = new EmbedFooter {
                    Text = "shibe.online"
                },
                Color = new DiscordColor(EmbedConstant.embedColor)
            };

            await context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("bomb"), Aliases("shibabomb", "shibebomb", "shibasbomb", "shibesbomb")]
        public async Task ShibaBombAsync(CommandContext context) {
            WebClient webClient = new WebClient();
            string jsonText = webClient.DownloadString("https://shibe.online/api/shibes?count=5");
            webClient.Dispose();

            await context.Channel.SendMessageAsync(string.Join('\n', JArray.Parse(jsonText)));
        }
        
        [Command("avatar"), Aliases("pfp")]
        public async Task AvatarAsync(CommandContext context, [RemainingText] DiscordUser user = null) {
            user ??= context.User;
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                ImageUrl = user.AvatarUrl,
                Title = $"{user.Username}#{user.Discriminator}",
                Color = new DiscordColor(EmbedConstant.embedColor)
            };

            await context.Channel.SendMessageAsync(embed: builder.Build());
        }
        
        [Command("husky")]
        public async Task HuskyAsync(CommandContext context) {
            WebClient webClient = new WebClient();
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder {
                ImageUrl = (string)JObject.Parse(webClient.DownloadString("https://dog.ceo/api/breed/husky/images/random"))["message"],
                Footer = new EmbedFooter {
                    Text = "dog.ceo"
                },
                Color = new DiscordColor(EmbedConstant.embedColor)
            };
            webClient.Dispose();
            await context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}