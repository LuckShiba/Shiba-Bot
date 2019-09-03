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
            public HelpModel Help { set; get; }

            public class ConfigurationModel {
                public string Locale { set; get; }
                public string InvalidLocale { set; get; }
                public string Prefix { set; get; }
                public List<string> InvalidPrefix { set; get; }
            }

            public class HelpModel {
                public string Help { set; get; }
                public string Commands { set; get; }
                public string CommandHelp { set; get; }
                public List<ModuleModel> Modules { set; get; }
                public class ModuleModel {
                    public string Name { set; get; }
                    public List<string> Commands { set; get; }
                    public string Emoji { set; get; }
                }
            }
        }
        public class CommandsUseModel {
            public string CommandUse { set; get; }
            public string Example { set; get; }
            public string Examples { set; get; }
            public List<CommandsModel> Commands { set; get; }
            public class CommandsModel {
                public string Name { set; get; }
                public List<string> Aliases { set; get; }
                public string Description { set; get; }
                public int Use { set; get; }
                public List<string> Strings { set; get; }
            }
        }
    }
}