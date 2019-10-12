using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;

namespace ShibaBot.Data.MySQL.DAO {
    public class GuildsDAO {
        private readonly MySqlConnection connection = MySQLConnectionFactory.Connect();

        public async Task<string> GetLocaleAsync(ulong ID) {
            MySqlCommand query = new MySqlCommand("GetLocale", connection) {
                CommandType = CommandType.StoredProcedure
            };
            query.Parameters.AddWithValue("_ID", ID);
            query.Parameters.Add("_Locale", MySqlDbType.VarChar);
            query.Parameters["_Locale"].Direction = ParameterDirection.Output;
            (await query.ExecuteReaderAsync()).Close();
            connection.Close();
            query.Dispose();
            return query.Parameters["_Locale"].Value.ToString();
        }

        public async Task<string> GetPrefixAsync(ulong ID) {
            MySqlCommand query = new MySqlCommand("GetPrefix", connection) {
                CommandType = CommandType.StoredProcedure
            };
            query.Parameters.AddWithValue("_ID", ID);
            query.Parameters.Add("_Prefix", MySqlDbType.VarChar);
            query.Parameters["_Prefix"].Direction = ParameterDirection.Output;
            (await query.ExecuteReaderAsync()).Close();
            connection.Close();
            query.Dispose();
            return query.Parameters["_Prefix"].Value.ToString();
        }

        public async Task UpdateLocaleAsync(ulong ID, string Locale) {
            MySqlCommand query = new MySqlCommand("call UpdateLocale(@ID, @Locale)", connection);
            query.Parameters.AddWithValue("@ID", ID);
            query.Parameters.AddWithValue("@Locale", Locale);
            await query.ExecuteNonQueryAsync();
            connection.Close();
            query.Dispose();
        }

        public async Task UpdatePrefixAsync(ulong ID, string Prefix) {
            MySqlCommand query = new MySqlCommand("call UpdatePrefix(@ID, @Prefix)", connection);
            query.Parameters.AddWithValue("@ID", ID);
            query.Parameters.AddWithValue("@Prefix", Prefix);
            await query.ExecuteNonQueryAsync();
            connection.Close();
            query.Dispose();
        }
    }
}
