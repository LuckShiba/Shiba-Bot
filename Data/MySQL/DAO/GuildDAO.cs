using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data.Common;
using System;

namespace ShibaBot.Data.MySQL.DAO {
    public class GuildDAO {
        private readonly MySqlConnection connection = MySQLConnectionFactory.Connect();

        public async Task<int> GetLocaleAsync(ulong ID) {
            MySqlCommand command = new MySqlCommand("call GetLocale(@ID)", connection);
            command.Parameters.AddWithValue("@ID", ID);
            DbDataReader reader = await command.ExecuteReaderAsync();
            connection.Close();
            try {
                if (await reader.ReadAsync()) {
                    return Convert.ToInt32(reader["Locale"]);
                }
                return 0;
            }
            finally {
                connection.Close();
            }
        }

        public async Task<string> GetPrefixAsync(ulong ID) {
            MySqlCommand command = new MySqlCommand("call GetPrefix(@ID)", connection);
            command.Parameters.AddWithValue("@ID", ID);
            DbDataReader reader = await command.ExecuteReaderAsync();
            try {
                if (await reader.ReadAsync()) {
                    return reader["Prefix"].ToString();
                }
                return "s.";
            }
            finally {
                connection.Close();
            }
        }

        public async Task UpdateLocaleAsync(ulong ID, int Locale) {
            MySqlCommand command = new MySqlCommand("call SetLocale(@ID, @Locale)", connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Locale", Locale);
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task UpdatePrefixAsync(ulong ID, string Prefix) {
            MySqlCommand command = new MySqlCommand("call SetPrefix(@ID, @Prefix)", connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Prefix", Prefix);
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }
    }
}
