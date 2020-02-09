using System.Threading.Tasks;
using ShibaBot.Data.MySQL.DAO;
using Discord.Commands;
using ShibaBot.Models;
using Newtonsoft.Json;
using System.IO;
using System;

namespace ShibaBot.Singletons {
    public static class Language {
        public readonly static LocalesModel en_US = JsonConvert.DeserializeObject<LocalesModel>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Data/Locales/en-US.json"));
        public readonly static LocalesModel pt_BR = JsonConvert.DeserializeObject<LocalesModel>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Data/Locales/pt-BR.json"));

        public static async Task<LocalesModel> GetLanguageAsync(CommandContext context) {
            if (!context.IsPrivate) {
                switch (await new GuildDAO().GetLocaleAsync(context.Guild.Id)) {
                    case 1:
                        return en_US;

                    case 2:
                        return pt_BR;
                }
            }
            return en_US;
        }
    }
}