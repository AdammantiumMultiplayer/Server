using AMP;
using AMP.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
