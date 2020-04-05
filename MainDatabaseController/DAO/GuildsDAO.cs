using MainDatabaseController.Singletons;
using MainDatabaseController.Models;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MainDatabaseController.DAO {
    public class GuildsDAO {
        private readonly IMongoCollection<GuildsModel> _collection = DatabaseSingleton.database.GetCollection<GuildsModel>("guilds");

        public async Task<GuildsModel> GetAsync(GuildsModel guild) {
            GuildsModel dbGuild = await (await _collection.FindAsync(x => x.ID == guild.ID)).FirstOrDefaultAsync();

            return new GuildsModel { ID = guild.ID, Locale = dbGuild?.Locale, Prefix = dbGuild?.Prefix };
        }

        public async Task SetAsync(GuildsModel guild) {
            UpdateDefinition<GuildsModel> update = new BsonDocument();

            if (guild.Locale != null)
                update = update.Set(x => x.Locale, guild.Locale);
            if (guild.Prefix != null)
                update = update.Set(x => x.Prefix, guild.Prefix);

            await _collection.UpdateOneAsync(x => x.ID == guild.ID, update, new UpdateOptions { IsUpsert = true });
        }
    }
}
