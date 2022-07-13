using AMP;
using AMP.Logging;
using System.Threading;

namespace AMP_Server {
    internal class Program {
        static void Main(string[] args) {
            Log.loggerType = Log.LoggerType.CONSOLE;

            ModManager.HostServer(10, 26950, false);

            while(ModManager.serverInstance != null) {
                Thread.Sleep(1000);
            }

        }
    }
}
