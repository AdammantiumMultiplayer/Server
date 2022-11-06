using AMP.Logging;
using AMP.Network.Data;
using AMP.Network.Server;
using System;

namespace AMP.DedicatedServer {
    internal class PluginEventHandler {

        internal static void RegisterEvents() {
            Server.OnClientJoin += InvokeOnClientJoin;
            Server.OnClientQuit += InvokeOnClientQuit;
        }

        internal static void InvokeOnClientJoin(ClientData client) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnClientJoin(client);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        internal static void InvokeOnClientQuit(ClientData client) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnClientQuit(client);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }


    }
}
