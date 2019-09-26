using Microsoft.Data.Sqlite;
using System;

namespace ShibaBot.Data.Sqlite {
    public static class SqliteConnectionFactory {
        public static SqliteConnection Connect() {
            SqliteConnection connection = new SqliteConnection($"Data Source={AppDomain.CurrentDomain.BaseDirectory}Data/Sqlite/LocalDB.db");

            connection.Open();
            return connection;
        }
    }
}
