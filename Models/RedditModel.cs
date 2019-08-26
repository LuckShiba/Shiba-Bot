using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShibaBot.Models {
    public class RedditModel {

        [JsonProperty("data")]
        public DataModel Data { set; get; }
        public class DataModel {
            [JsonProperty("children")]
            public List<ChildrenModel> Children { set; get; }
            public class ChildrenModel {
                [JsonProperty("data")]
                public DataModel Data { set; get; }
                public class DataModel {
                    [JsonProperty("title")]
                    public string Title { set; get; }
                    [JsonProperty("author")]
                    public string Author { set; get; }
                    [JsonProperty("url")]
                    public string Url { set; get; }
                    [JsonProperty("permalink")]
                    public string PermaLink { set; get; }
                }
            }
        }
    }
}
