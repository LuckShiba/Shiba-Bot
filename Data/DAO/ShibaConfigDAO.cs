using Microsoft.Data.Sqlite;
using ShibaBot.Models;
using System;

namespace ShibaBot.Data.DAO {
    public class ShibaConfigDAO {
        private SqliteConnection connection = new ConnectionFactory().Connect();

        public ShibaConfigModel Load() {
            SqliteCommand query = connection.CreateCommand();
            query.CommandText += "select * from ShibaConfig where ID = @ID";
            query.Parameters.AddWithValue("@ID", ShibaConfigModel.ID);

            using (SqliteDataReader reader = query.ExecuteReader()) {
                ShibaConfigModel shibaConfig = null;
                while (reader.Read()) {
                    shibaConfig = new ShibaConfigModel(reader["Token"].ToString(), Convert.ToUInt64(reader["OwnerID"]));
                }
                reader.Close();
                connection.Close();
                return shibaConfig;
            }
        }
    }
}
