using AMP;
using AMP.Logging;
using AMP.Threading;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace AMP_Server {
    internal class Program {
        static void Main(string[] args) {
            Log.loggerType = Log.LoggerType.CONSOLE;

            Dispatcher umtd = new Dispatcher();
            Dispatcher.current = umtd;

            ModManager.HostServer(10, 26950);

            ModManager.discordNetworking = false;

            while(ModManager.serverInstance != null) {
                Thread.Sleep(1);
                umtd.ServerUpdateTick();
            }

        }
    }
}
