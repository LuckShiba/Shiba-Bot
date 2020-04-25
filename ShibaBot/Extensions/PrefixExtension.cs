using System.Threading.Tasks;
using MainDatabaseController.Models;

namespace ShibaBot.Extensions {
    internal class PrefixExtension {
        internal string GetPrefix(GuildsModel guild) {
            return guild?.Prefix ?? "s.";
        }
    }
}
