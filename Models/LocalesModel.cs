using System.Collections.Generic;

namespace ShibaBot.Models {
    public  class LocalesModel {
        public ErrorsModel Errors { set; get; }
        public string Mention { set; get; }
        public string MentionDM { set; get; }
        public ModulesModel Modules { set; get; }
        public CommandsUseModel CommandsUse { set; get; }

        public class ErrorsModel {
            public string BadArgCount { set; get; }
            public string ObjectNotFound { set; get; }
            public UnmetConditionModel UnmetCondition { set; get; }
            public ForbiddenModel Forbidden { set; get; }

            public class UnmetConditionModel {
                public string GuildOnly;
                public string UserManageGuild;
            }

            public class ForbiddenModel {
                public string EmbedLinks { set; get; }
            }
        }

        public class ModulesModel {    
            public ConfigurationModel Configuration { set; get; }

            public class ConfigurationModel {
                public string Locale { set; get; }
                public string InvalidLocale { set; get; }
                public string Prefix { set; get; }
                public string InvalidPrefix { set; get; }
            }
        }
        public class CommandsUseModel {
            public string CommandUse { set; get; }
            public string Example { set; get; }
            public string shiba { set; get; }
            public string shibabomb { set; get; }
            public List<string> avatar { set; get; }
            public List<string> locale { set; get; }
            public List<string> setprefix { set; get; }
        }
    }
}