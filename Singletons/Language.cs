using System.Threading.Tasks;
using ShibaBot.Data.MySQL.DAO;
using Discord.Commands;
using ShibaBot.Models;
using Newtonsoft.Json;
using System.IO;
using System;

namespace ShibaBot.Singletons {
    public static class Language {
        private readonly static LocalesModel en_US = JsonConvert.DeserializeObject<LocalesModel>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Data\\Locales\\en-US.json"));
        private readonly static LocalesModel pt_BR = JsonConvert.DeserializeObject<LocalesModel>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Data\\Locales\\pt-BR.json"));

        public static async Task<LocalesModel> GetLanguageAsync(SocketCommandContext context) {
            if (!context.IsPrivate) {
                switch ((await new GuildsDAO().LoadAsync(context.Guild.Id)).Locale) {
                    case "en-US":
                        return en_US;

                    case "pt-BR":
                        return pt_BR;
                }
            }
            return en_US;
        }
    }
}
