using Microsoft.Data.Sqlite;
using System;

namespace ShibaBot.Data {
    public class ConnectionFactory {
        public SqliteConnection Connect() {
            SqliteConnection connection = new SqliteConnection($"Data Source={AppDomain.CurrentDomain.BaseDirectory}Data\\LocalDB.db");

            connection.Open();
            return connection;
        }
    }
}
