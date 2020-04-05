using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ConfigurationController.DAO {
    public class StringsDAO {
        private SqliteConnection _connection;
        public async Task<string> GetString(string identifier, int locale) {
            _connection = await ConnectionFactory.ConnectAsync();
            SqliteCommand query = _connection.CreateCommand();
            query.CommandText = "select String from Strings where Identifier = @Identifier and Locale = @Locale";
            query.Parameters.AddWithValue("@Identifier", identifier);
            query.Parameters.AddWithValue("@Locale", locale);

            SqliteDataReader reader = await query.ExecuteReaderAsync();

            string rString = null;

            if (await reader.ReadAsync()) {
                rString = reader["String"].ToString();
            }

            await _connection.CloseAsync();
            await _connection.DisposeAsync();

            return rString;
        }
    }
}
