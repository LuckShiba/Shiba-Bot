using ConfigurationController.Models;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System;

namespace ConfigurationController.DAO {
    public class StringsDAO {
        public async Task<StringsModel> LoadAsync(StringsModel stringsModel) {
            using SqliteConnection connection = await ConnectionFactory.ConnectAsync();
            SqliteCommand query = connection.CreateCommand();
            query.CommandText = "select String from Strings where Identifier = @Identifier and Locale = @Locale";
            query.Parameters.AddWithValue("@Identifier", stringsModel.Identifier);
            query.Parameters.AddWithValue("@Locale", stringsModel.Locale);

            SqliteDataReader reader = await query.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                stringsModel.String = (string)reader["String"];

            return stringsModel;
        }
    }
}
