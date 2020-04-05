using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ConfigurationController {
    internal static class ConnectionFactory {
        internal static async Task<SqliteConnection> ConnectAsync() {
            SqliteConnection connection = new SqliteConnection("Data Source=LocalDB.db");
            await connection.OpenAsync();
            return connection;
        }
    }
}
