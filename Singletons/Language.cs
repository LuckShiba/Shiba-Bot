using ShibaBot.Models;
using Discord.Commands;
using Newtonsoft.Json;
using System.IO;
using System;

namespace ShibaBot.Singletons {
    public static class Language {
        private static LocalesModel EN_US = JsonConvert.DeserializeObject<LocalesModel>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Data\\EN_US.json"));
        private static LocalesModel PT_BR = JsonConvert.DeserializeObject<LocalesModel>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Data\\PT_BR.json"));

        public static LocalesModel GetLanguage(SocketCommandContext context) {
            //...
            return PT_BR;
        }
    }
}
