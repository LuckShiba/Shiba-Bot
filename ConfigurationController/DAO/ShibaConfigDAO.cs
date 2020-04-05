using ConfigurationController.Models;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System;

namespace ConfigurationController.DAO {
    public class ShibaConfigDAO {
        private SqliteConnection _connection;

        public async Task<ShibaConfigModel> LoadAsync() {
            _connection = await ConnectionFactory.ConnectAsync();
            SqliteCommand query = _connection.CreateCommand();
            query.CommandText += "select * from ShibaConfig;";

            SqliteDataReader reader = await query.ExecuteReaderAsync();
            ShibaConfigModel shibaConfig = null;

            if (await reader.ReadAsync()) {
                shibaConfig = new ShibaConfigModel(Convert.ToUInt64(reader["OwnerID"]), reader["Token"].ToString(), reader["MongoConnectionString"].ToString());
            }

            await _connection.CloseAsync();
            await _connection.DisposeAsync();

            return shibaConfig;
        }
    }
}
