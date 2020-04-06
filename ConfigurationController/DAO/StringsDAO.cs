using ConfigurationController.Models;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System;

namespace ConfigurationController.DAO {
    public class StringsDAO {
        private SqliteConnection _connection;
        public async Task<StringsModel> LoadAsync(StringsModel stringsModel) {
            _connection = await ConnectionFactory.ConnectAsync();
            SqliteCommand query = _connection.CreateCommand();
            query.CommandText = "select * from Strings where Identifier = @Identifier and Locale = @Locale";
            query.Parameters.AddWithValue("@Identifier", stringsModel.Identifier);
            query.Parameters.AddWithValue("@Locale", stringsModel.Locale);

            SqliteDataReader reader = await query.ExecuteReaderAsync();

            StringsModel dbStringsModel = null;

            if (await reader.ReadAsync())
                dbStringsModel = new StringsModel { ID = Convert.ToInt32(reader["ID"]), Identifier = reader["Identifier"].ToString(), Locale = Convert.ToInt32(reader["Locale"]), String = reader["String"].ToString() };

            await _connection.CloseAsync();
            await _connection.DisposeAsync();

            return dbStringsModel;
        }
    }
}
