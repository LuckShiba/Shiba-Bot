using Microsoft.Data.Sqlite;
using ShibaBot.Models;
using System.Runtime.InteropServices;

namespace ShibaBot.Data.Sqlite.DAO {
    public class MySQLConfigDAO {
        private SqliteConnection connection = new SqliteConnectionFactory().Connect();

        public MySQLConfigModel Load() {
            SqliteCommand query = connection.CreateCommand();
            query.CommandText += "select * from MySQLConfig where ID = @ID";
            query.Parameters.AddWithValue("@ID", RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? 1 : 2);

            using (SqliteDataReader reader = query.ExecuteReader()) {
                MySQLConfigModel mySQLConfig= null;
                while (reader.Read()) {
                    mySQLConfig = new MySQLConfigModel(reader["IP"].ToString(), reader["Database"].ToString(), reader["User"].ToString(), reader["Password"].ToString());
                }
                reader.Close();
                connection.Close();
                return mySQLConfig;
            }
        }
    }
}
