namespace ShibaBot.Models {
    public  class LocalesModel {
        public ErrorsModel Errors;
        public string Mention;
        public ModulesModel Modules;

        public class ErrorsModel {
            public string BadArgCount { set; get; }
            public string ObjectNotFound { set; get; }
            public string UnknownCommand { set; get; }
            public UnmetConditionModel UnmetCondition { set; get; }
        }

        public class UnmetConditionModel {
            public string GuildOnly;
            public string UserManageGuild;
        }
        public class ModulesModel {    
            public ConfigurationModel Configuration { set; get; }

            public class ConfigurationModel {
                public string Locale { set; get; }
                public string InvalidLocale { set; get; }
            }
        }
    }
}