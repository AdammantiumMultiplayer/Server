using AMP;
using AMP.Data;
using AMP.Logging;
using AMP.Threading;
using System;

namespace AMP_Server {
    internal class Program {
        static void Main(string[] args) {
            Log.loggerType = Log.LoggerType.CONSOLE;

            ServerConfig.path = "server.ini";
            ServerConfig.Load();

            ModManager.HostDedicatedServer((uint) ServerConfig.maxPlayers, 26950);
        }
    }
}
