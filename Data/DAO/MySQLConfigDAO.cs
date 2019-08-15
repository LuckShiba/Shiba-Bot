using Microsoft.Data.Sqlite;
using ShibaBot.Models;

namespace ShibaBot.Data.DAO {
    public class MySQLConfigDAO {
        private SqliteConnection connection = new ConnectionFactory().Connect();

        public MySQLConfigModel Load() {
            SqliteCommand query = connection.CreateCommand();
            query.CommandText += "select * from MySQLConfig where ID = @ID";
            query.Parameters.AddWithValue("@id", MySQLConfigModel.ID);

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
