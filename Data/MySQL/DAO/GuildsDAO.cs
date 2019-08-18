using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using ShibaBot.Models;
using System.Data.Common;

namespace ShibaBot.Data.MySQL.DAO {
    public class GuildsDAO {
        private readonly MySqlConnection connection = new MySQLConnectionFactory().Connect();

        public async Task<GuildsModel> LoadAsync(ulong guildId) {
            MySqlCommand query = new MySqlCommand("select * from Guilds where ID = @ID", connection);
            query.Parameters.AddWithValue("@ID", guildId);

            DbDataReader reader = await query.ExecuteReaderAsync();

            GuildsModel guilds;

            if (reader.Read()) {
                guilds = new GuildsModel(guildId, reader["Locale"].ToString());
                reader.Close();
            }

            else {
                reader.Close();

                MySqlCommand queryInsert = new MySqlCommand("insert into Guilds values(@ID, 'en-US')", connection);
                queryInsert.Parameters.AddWithValue("@ID", guildId);
                await queryInsert.ExecuteNonQueryAsync();
                queryInsert.Dispose();

                guilds = new GuildsModel(guildId, "en-US");
            }

            connection.Close();
            query.Dispose();

            return guilds;
        }

        public void UpdateLocale(ulong ID, string Locale) {
            MySqlCommand query = new MySqlCommand("call UpdateLocale(@ID, @Locale)", connection);
            query.Parameters.AddWithValue("@ID", ID);
            query.Parameters.AddWithValue("@Locale", Locale);
        }
    }
}
