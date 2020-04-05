using ConfigurationController.DAO;
using System.Threading.Tasks;
using ShibaBot;
using System;

namespace ConsoleGUI {
    class Launcher {
        async static Task Main() {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("   _____ _     _ _           ____        _   \n  / ____| |   (_) |         |  _ \\      | |  \n | (___ | |__  _| |__   __ _| |_) | ___ | |_ \n  \\___ \\| '_ \\| | '_ \\ / _` |  _ < / _ \\| __|\n  ____) | | | | | |_) | (_| | |_) | (_) | |_ \n |_____/|_| |_|_|_.__/ \\__,_|____/ \\___/ \\__|");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" by LuckShiba\n");

            await new Main(await new ShibaConfigDAO().LoadAsync()).RunAsync();
        }
    }
}
