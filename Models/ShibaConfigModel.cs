using System;
using System.Collections.Generic;
using System.Text;

namespace ShibaBot.Models {
    public class ShibaConfigModel {
        public const uint ID = 1;
        public string Token { get; private set; }
        public ulong OwnerID { get; private set; }

        public ShibaConfigModel(string Token, ulong OwnerID) {
            this.Token = Token;
            this.OwnerID = OwnerID;
        }
    }
}