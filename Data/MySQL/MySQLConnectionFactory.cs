using ShibaBot.Models;
using MySql.Data.MySqlClient;
using ShibaBot.Data.Sqlite.DAO;
namespace ShibaBot.Data.MySQL {
    public class MySQLConnectionFactory {
        public static MySqlConnection Connect() {
            MySQLConfigModel mySQLConfig = new MySQLConfigDAO().Load();
            MySqlConnection connection = new MySqlConnection($"Server={mySQLConfig.IP};Database={mySQLConfig.Database};Uid={mySQLConfig.User};Pwd={mySQLConfig.Password};");
            connection.Open();
            return connection;
        }
    }
}
