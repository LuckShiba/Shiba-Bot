using System;

namespace ShibaBot.Attributes {
    public class ModuleAttribute : Attribute {
        public string Name { get; private set; }
        public ModuleAttribute(string name) {
            Name = name;
        }
    }
}
