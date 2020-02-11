namespace ShibaBot.Models {
    public class MySQLConfigModel {
        public uint ID;
        public string IP { get; private set; }
        public string Port { get; private set; }
        public string Database { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }
        public MySQLConfigModel(string IP, string port, string database, string user, string password) {
            this.IP = IP;
            Port = port;
            Database = database;
            User = user;
            Password = password;
        }
    }
}
