using static DSharpPlus.Entities.DiscordEmbedBuilder;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DSharpPlus.Entities;
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
        [Description("BombDescription")]
        public async Task BombAsync(CommandContext context) {
            WebClient webClient = new WebClient();
            string jsonText = webClient.DownloadString("https://shibe.online/api/shibes?count=5");
            webClient.Dispose();

            await context.Channel.SendMessageAsync(string.Join('\n', JArray.Parse(jsonText)));
        }
    }
}
