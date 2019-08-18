﻿namespace ShibaBot.Models {
    public class MySQLConfigModel {
        public uint ID;
        public string IP { get; private set; }
        public string Database { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }

        public MySQLConfigModel(string IP, string Database, string User, string Password) {
            this.IP = IP;
            this.Database = Database;
            this.User = User;
            this.Password = Password;
        }
    }
}
